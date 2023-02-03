using System.Collections.Generic;
using Banks.Library.Public;
using Banks.Library.Public.Enums;
using Banks.Library.Public.RequestModels;

namespace Banks.Tests 
{
    internal static class BanksTestsFactory
    {
        internal static BankServiceManager CreateThreeEmptyBanks()
        {
            var bankServiceManager = new BankServiceManager();
            bankServiceManager.AddBank("FossilsBank");
            bankServiceManager.AddBank("TriassicBank");
            bankServiceManager.AddBank("JurassicBank");

            return bankServiceManager;
        }

        internal static BankServiceManager CreateThreeBanksWithAllAccountTypesEach()
        {
            var bankServiceManager = new BankServiceManager();

            var bankListToAddTo = new List<string> {"FossilsBank", "TriassicBank", "JurassicBank"};
            bankListToAddTo.ForEach(bankName => 
            {
                bankServiceManager.AddBank(bankName);

                var newAccountRequest1 = new NewAccountRequest 
                {
                    BankName = bankName,
                    HolderName = "Trilo Bites",
                    AccountType = AccountType.Checking
                };

                var newAccountRequest2 = new NewAccountRequest 
                {
                    BankName = bankName,
                    HolderName = "Brachio Pods",
                    AccountType =  AccountType.InvestmentCorporate
                };

                var newAccountRequest3 = new NewAccountRequest 
                {
                    BankName = bankName,
                    HolderName = "Iso Pods",
                    AccountType = AccountType.InvestmentIndividual
                };

                bankServiceManager.AddAccount(newAccountRequest1);
                bankServiceManager.AddAccount(newAccountRequest2);
                bankServiceManager.AddAccount(newAccountRequest3);
            });

            return bankServiceManager;
        }
    }
}

