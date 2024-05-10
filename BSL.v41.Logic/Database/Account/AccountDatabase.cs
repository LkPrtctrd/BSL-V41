using Newtonsoft.Json;
using BSL.v41.Titan.Graphic;

namespace BSL.v41.Logic.Database.Account;

public class AccountDatabase : AccountModel
{
    private readonly string? _filePath;
    private Dictionary<long, AccountModel> _onRamBase;

    public AccountDatabase(string? filePath, bool loadByBackup = false)
    {
        _filePath = filePath;
        _onRamBase = new Dictionary<long, AccountModel>();
    }

    public void CyclingLoadDataFromFile(int perSec)
    {
        Task.Run(() =>
        {
            while (true)
            {
                Thread.Sleep(perSec * 1000);
                {
                    LoadDataFromFile(true);
                }
            }
        });
    }

    public void LoadDataFromFile(bool reloaded = false)
    {
        try
        {
            var data = File.ReadAllText(_filePath!);
            {
                _onRamBase = ConvertJsonToDictionary(data);
            }

            ConsoleLogger.WriteTextWithPrefix(!reloaded ? ConsoleLogger.Prefixes.Start : ConsoleLogger.Prefixes.Load,
                $"Database-element {(reloaded ? "reloaded" : "started")}! Information: element name: {GetType().Name}; " +
                $"database buffer length: {_onRamBase.Count}.");
        }
        catch (Exception exception)
        {
            ConsoleLogger.WriteTextWithPrefix(ConsoleLogger.Prefixes.Error, exception);
        }
    }

    public AccountModel CreateAccount(AccountModel accountModel, long accountId = -1)
    {
        accountId = accountId < 0 ? GetNewAccountId() : accountId;
        {
            try
            {
                OnAssistByAccountInFile(accountModel, false, accountId);
                {
                    accountModel.SetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AccountId,
                        accountId);
                }
            }
            catch
            {
                // ignored.
            }
        }

        return accountModel;
    }

    public AccountModel LoadAccount(long id)
    {
        try
        {
            return _onRamBase[id];
        }
        catch (Exception)
        {
            // ignored.
        }

        return null!;
    }

    public AccountModel SetAccountParameter(long accountId, AccountStructure accountStructure, object value)
    {
        var accountModel = LoadAccount(accountId);
        if (accountModel == null!) return null!;

        var parameterName = AccountHelper.GetNormalParameterNameFromAccountStructure(accountStructure);
        {
            if (accountModel != null!) ReplaceFieldValueByFieldName(accountModel, parameterName, value);
        }

        return OnAssistByAccountInFile(accountModel!, true, accountId);
    }

    public object GetAccountParameter(long accountId, AccountStructure accountStructure)
    {
        var accountModel = LoadAccount(accountId);
        {
            if (accountModel != null!)
                return accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(accountStructure);
        }

        return null!;
    }

    public AccountModel OnAssistByAccountInFile(AccountModel accountModel, bool wasReplace, long accountId = -1)
    {
        accountId = Convert.ToInt64(accountId < 0
            ? accountModel.GetFieldValueByAccountStructureParameterFromAccountModel(AccountStructure.AccountId)
            : accountId);

        var jsonData = File.ReadAllText(_filePath!);
        {
            var accountData = JsonConvert.DeserializeObject<Dictionary<long, AccountModel>>(jsonData);
            {
                var newAccountModel = new AccountModel();
                {
                    switch (wasReplace)
                    {
                        case true:
                            _onRamBase[accountId] = accountModel;
                            accountData![accountId] = accountModel;
                        {
                            AbstractingAccountModelFromRootAccountModel(newAccountModel, accountModel);
                        }
                            break;
                        case false:
                            _onRamBase.Add(accountId, accountModel);
                        {
                            accountData!.Add(Convert.ToInt32(accountId.ToString()), accountModel);
                        }
                            break;
                    }
                }

                if (accountData == null!) return accountModel;

                File.WriteAllText(_filePath!, JsonConvert.SerializeObject(accountData, Formatting.Indented));
                return newAccountModel;
            }
        }
    }

    public string ConvertDictionaryToJson(Dictionary<long, AccountModel>? onRamBase = null!)
    {
        return JsonConvert.SerializeObject(onRamBase ?? _onRamBase);
    }

    public Dictionary<long, AccountModel> ConvertJsonToDictionary(string json, bool localUs = false)
    {
        return localUs ? _onRamBase : JsonConvert.DeserializeObject<Dictionary<long, AccountModel>>(json)!;
    }

    public long GetMaxAccountId()
    {
        return _onRamBase.Count == 0 ? 0 : _onRamBase.Keys.Max();
    }

    public long GetNewAccountId()
    {
        return GetMaxAccountId() + 1;
    }
}