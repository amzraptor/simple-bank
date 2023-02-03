using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Banks.Library.Public;
using Banks.Library.Public.Exceptions;
using static Banks.Tests.BanksTestsFactory;
using Banks.Library.Public.Enums;
using Banks.Library.Public.RequestModels;
using System.Collections.Generic;
using System.Linq;

namespace Banks.Tests
{
    [TestClass]
    public class BankServiceManagerUnitTests
    {
        [TestMethod]
        public void AddBank_Should_AddBank()
        {
            //arrange
            var bankServiceManager = new BankServiceManager();
            var bank1Name = "FossilsBank";

            //act
            bankServiceManager.AddBank(bank1Name);

            //assert
            var existingBankNames = bankServiceManager.ListBanks();
            Assert.AreEqual(1, existingBankNames.Count);
            Assert.IsTrue(existingBankNames.Contains(bank1Name));

        }

        [TestMethod]
        public void AddMultipleBanks_Should_AddMultipleBanks()
        {
            //arrange
            var bankServiceManager = new BankServiceManager();
            var bank1Name = "FossilsBank";
            var bank2Name = "TriassicBank";

            //act
            bankServiceManager.AddBank(bank1Name);
            bankServiceManager.AddBank(bank2Name);

            //assert
            var existingBankNames = bankServiceManager.ListBanks();
            Assert.AreEqual(2, existingBankNames.Count);
            Assert.IsTrue(existingBankNames.Contains(bank1Name));
            Assert.IsTrue(existingBankNames.Contains(bank2Name));

        }



        [TestMethod]
        [ExpectedException(typeof(DuplicateBankException))]
        public void AddBank_Should_PreventDuplicates()
        {
            //arrange
            var bankServiceManager = new BankServiceManager();
            var bank1Name = "FossilsBank";

            //act
            bankServiceManager.AddBank(bank1Name);
            bankServiceManager.AddBank(bank1Name);

            //assert
            //done through method attribute!
        }

        [TestMethod]
        public void AddBank_Should_PreventDuplicatesAndShowDuplicateKeyThroughInnerException()
        {
            //arrange
            var bankServiceManager = new BankServiceManager();
            var bank1Name = "FossilsBank";

            //act

            try
            {

                bankServiceManager.AddBank(bank1Name);
                bankServiceManager.AddBank(bank1Name);
                Assert.Fail("Should Not Reach here");
            }
            catch(DuplicateBankException ex) 
            {
                //assert
                Assert.IsTrue(ex.InnerException.Message.IndexOf(bank1Name, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            catch(Exception ex) 
            {
                //assert
                Assert.Fail("Incorrect exception thrown");
            }
        }

        [TestMethod]
        public void AddAccountToBank_Should_AddAccountToBank() 
        {
            //arrange
            var bankServiceManager = CreateThreeEmptyBanks();

            var bankNameForAccount = "FossilsBank";
            var accountHolderName = "Trilo Bites";
            var accountType = AccountType.Checking;

            var newAccountRequest = new NewAccountRequest 
            {
                BankName = bankNameForAccount,
                HolderName = accountHolderName,
                AccountType = accountType
            };

            //act
            bankServiceManager.AddAccount(newAccountRequest);

            //assert
            var accountResults = bankServiceManager.ListAccounts();
            Assert.AreEqual(1, accountResults.Count);
            Assert.IsNotNull(accountResults[0].AccountId);
            Assert.AreEqual(accountHolderName, accountResults[0].HolderName);
            Assert.AreEqual(bankNameForAccount, accountResults[0].BankName);
            Assert.AreEqual(accountType, accountResults[0].AccountType);

        }

        [TestMethod]
        [ExpectedException(typeof(UnknownBankException))]
        public void AddAccountToBankThatDoesNotExist_Should_Fail() 
        {
            //arrange
            var bankServiceManager = CreateThreeEmptyBanks();

            var bankNameForAccount = "MileyCyrusBank";
            var accountHolderName = "Trilo Bites";
            var accountType = AccountType.Checking;

            var newAccountRequest = new NewAccountRequest 
            {
                BankName = bankNameForAccount,
                HolderName = accountHolderName,
                AccountType = accountType
            };

            //act
            bankServiceManager.AddAccount(newAccountRequest);

            //assert
            //done through method attribute!

        }

        [TestMethod]
        public void AddAccountToBankThatDoesNotExist_Should_FailAndShowUnknownNameInInnerException() 
        {
            //arrange
            var bankServiceManager = CreateThreeEmptyBanks();

            var bankNameForAccount = "MileyCyrusBank";
            var accountHolderName = "Trilo Bites";
            var accountType = AccountType.Checking;

            var newAccountRequest = new NewAccountRequest 
            {
                BankName = bankNameForAccount,
                HolderName = accountHolderName,
                AccountType = accountType
            };

            //act
            try
            {
                bankServiceManager.AddAccount(newAccountRequest);
                Assert.Fail("Should Not Reach here");
            }
            catch(UnknownBankException ex) 
            {
                //assert
                Assert.IsTrue(ex.InnerException.Message.IndexOf(bankNameForAccount, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            catch(Exception ex) 
            {
                //assert
                Assert.Fail("Incorrect exception thrown");
            }

        }

        [TestMethod]
        public void AddMultipleAccountsToSameBank_Should_AddMultipleAccountsToSameBank() 
        {
            //arrange
            var bankServiceManager = CreateThreeEmptyBanks();

            var bankNameForAccount = "FossilsBank";
            var accountHolderName1 = "Trilo Bites";
            var accountHolderName2 = "Brachio Pods";
            var accountType1 = AccountType.Checking;
            var accountType2 = AccountType.InvestmentCorporate;

            var newAccountRequest1 = new NewAccountRequest 
            {
                BankName = bankNameForAccount,
                HolderName = accountHolderName1,
                AccountType = accountType1
            };

             var newAccountRequest2 = new NewAccountRequest 
            {
                BankName = bankNameForAccount,
                HolderName = accountHolderName2,
                AccountType = accountType2
            };

            //act
            bankServiceManager.AddAccount(newAccountRequest1);
            bankServiceManager.AddAccount(newAccountRequest2);

            //assert
            var accountResults = bankServiceManager.ListAccounts();
            Assert.AreEqual(2, accountResults.Count);

            Assert.IsNotNull(accountResults[0].AccountId);
            Assert.AreEqual(accountHolderName1, accountResults[0].HolderName);
            Assert.AreEqual(bankNameForAccount, accountResults[0].BankName);
            Assert.AreEqual(accountType1, accountResults[0].AccountType);

            Assert.IsNotNull(accountResults[1].AccountId);
            Assert.AreEqual(accountHolderName2, accountResults[1].HolderName);
            Assert.AreEqual(bankNameForAccount, accountResults[1].BankName);
            Assert.AreEqual(accountType2, accountResults[1].AccountType);            

        }

        [TestMethod]
        public void AddMultipleAccountsToDifferentBanks_Should_AddMultipleAccountsToDifferentBanks() 
        {
            //arrange
            var bankServiceManager = CreateThreeEmptyBanks();

            var bankListToAddTo = new List<string> {"FossilsBank", "TriassicBank", "JurassicBank"};

            var accountHolderName1 = "Trilo Bites";
            var accountHolderName2 = "Brachio Pods";
            var accountType1 = AccountType.Checking;
            var accountType2 = AccountType.InvestmentCorporate;

            bankListToAddTo.ForEach(bankName => 
            {

                var newAccountRequest1 = new NewAccountRequest 
                {
                    BankName = bankName,
                    HolderName = accountHolderName1,
                    AccountType = accountType1
                };

                var newAccountRequest2 = new NewAccountRequest 
                {
                    BankName = bankName,
                    HolderName = accountHolderName2,
                    AccountType = accountType2
                };

                //act
                bankServiceManager.AddAccount(newAccountRequest1);
                bankServiceManager.AddAccount(newAccountRequest2);
            });
            

            //assert
            var accountResults = bankServiceManager.ListAccounts();
            Assert.AreEqual(6, accountResults.Count);   

            var i = 0; //an assert counter
            bankListToAddTo.ForEach(bankName => 
            {
                Assert.IsNotNull(accountResults[0+i].AccountId);
                Assert.AreEqual(accountHolderName1, accountResults[0+i].HolderName);
                Assert.AreEqual(bankName, accountResults[0+i].BankName);
                Assert.AreEqual(accountType1, accountResults[0+i].AccountType);

                Assert.IsNotNull(accountResults[1+i].AccountId);
                Assert.AreEqual(accountHolderName2, accountResults[1+i].HolderName);
                Assert.AreEqual(bankName, accountResults[1+i].BankName);
                Assert.AreEqual(accountType2, accountResults[1+i].AccountType);
                i += 2; // this is just how many we are adding per bank   
            });

        }

        [DataRow(AccountType.Checking)]
        [DataRow(AccountType.InvestmentCorporate)]
        [DataRow(AccountType.InvestmentIndividual)]
        [DataTestMethod]
        public void DepositAnyAmountOnAllAccountsTypes_Should_ShowCorrectBalance(AccountType accountType)
        {
             //arrange
            var bankServiceManager = CreateThreeBanksWithAllAccountTypesEach();
            var accountsForTransaction = bankServiceManager.ListAccounts().Where(a => a.AccountType == accountType).ToList();

           
            accountsForTransaction.ForEach(a => 
            {
                Random rnd = new Random();
                var randAmount1 = rnd.Next(1, 10000);
                var randAmount2 = rnd.Next(1, 10000);

                 //act
                bankServiceManager.Deposit(new DepositRequest
                {
                    AccountId = a.AccountId,
                    Amount = randAmount1

                });
                bankServiceManager.Deposit(new DepositRequest
                {
                    AccountId = a.AccountId,
                    Amount = randAmount2

                });

                var transactionAccount = bankServiceManager.ListAccounts().Find(ta => ta.AccountId == a.AccountId);
                
                //assert
                var expectedAmount = Convert.ToDecimal(randAmount1 + randAmount2);
                Assert.AreEqual(expectedAmount, transactionAccount.Balance);
                Assert.AreEqual(2, transactionAccount.Transactions.Count);
            });        
        }

        [DataRow(AccountType.Checking)]
        [DataRow(AccountType.InvestmentCorporate)]
        [DataRow(AccountType.InvestmentIndividual)]
        [DataTestMethod]
        public void WithdrawAmountUnderOrEqualTo500OnAllAccountsTypes_Should_ShowCorrectBalance(AccountType accountType)
        {
             //arrange
            var bankServiceManager = CreateThreeBanksWithAllAccountTypesEach();
            var accountsForTransaction = bankServiceManager.ListAccounts().Where(a => a.AccountType == accountType).ToList();

           
            accountsForTransaction.ForEach(a => 
            {
                Random rnd = new Random();
                var randAmount = rnd.Next(1, 500);

                 //act
                bankServiceManager.Withdraw(new WithdrawRequest
                {
                    AccountId = a.AccountId,
                    Amount = randAmount

                });

                var transactionAccount = bankServiceManager.ListAccounts().Find(ta => ta.AccountId == a.AccountId);
                
                //assert
                var expectedAmount = Convert.ToDecimal(-randAmount);
                Assert.AreEqual(expectedAmount, transactionAccount.Balance);
                Assert.AreEqual(1, transactionAccount.Transactions.Count);
            });        
        }
        [DataRow(AccountType.Checking)]
        [DataRow(AccountType.InvestmentCorporate)]
        [DataTestMethod]
        public void WithdrawAmountOver500OnCheckingAndInvestmentCorporate_Should_ShowCorrectBalance(AccountType accountType)
        {
             //arrange
            var bankServiceManager = CreateThreeBanksWithAllAccountTypesEach();
            var accountsForTransaction = bankServiceManager.ListAccounts().Where(a => a.AccountType == accountType).ToList();

           
            accountsForTransaction.ForEach(a => 
            {
                Random rnd = new Random();
                var randAmount1 = rnd.Next(501, 10000);
                 var randAmount2 = rnd.Next(501, 10000);

                 //act
                bankServiceManager.Withdraw(new WithdrawRequest
                {
                    AccountId = a.AccountId,
                    Amount = randAmount1

                });
                bankServiceManager.Withdraw(new WithdrawRequest
                {
                    AccountId = a.AccountId,
                    Amount = randAmount2

                });

                var transactionAccount = bankServiceManager.ListAccounts().Find(ta => ta.AccountId == a.AccountId);
                
                //assert
                var expectedAmount = Convert.ToDecimal(-randAmount1 - randAmount2);
                Assert.AreEqual(expectedAmount, transactionAccount.Balance);
                Assert.AreEqual(2, transactionAccount.Transactions.Count);
            });        
        }

        
        [DataRow(AccountType.InvestmentIndividual)]
        [DataTestMethod]
        public void WithdrawAmountOver500OnInvestmentIndividual_Should_Fail(AccountType accountType)
        {
             //arrange
            var bankServiceManager = CreateThreeBanksWithAllAccountTypesEach();
            var accountsForTransaction = bankServiceManager.ListAccounts().Where(a => a.AccountType == accountType).ToList();

           
            accountsForTransaction.ForEach(a => 
            {
                Random rnd = new Random();
                var randAmount = rnd.Next(501, 10000);

                 //act
                try
                {
                    bankServiceManager.Withdraw(new WithdrawRequest
                    {
                        AccountId = a.AccountId,
                        Amount = randAmount

                    });
                    Assert.Fail("Should Not Reach here");
                }
                catch (WithdrawLimitReachedException wlre)
                {
                    var transactionAccount = bankServiceManager.ListAccounts().Find(ta => ta.AccountId == a.AccountId);
                    Assert.AreEqual(0, transactionAccount.Transactions.Count);                                        
                }
                catch(Exception ex) 
                {
                    //assert
                    Assert.Fail("Incorrect exception thrown");
                }
                
            });        
        }

        [TestMethod]
        public void TransferAmountUnderOrEqualTo500_Should_ShowWithdrawnAndDepositCorrectly() 
        {
            //arrange
             var bankServiceManager = CreateThreeBanksWithAllAccountTypesEach();

             var originAccountId = bankServiceManager.ListAccounts().First().AccountId; 
             var destinationAccountId =  bankServiceManager.ListAccounts().Last().AccountId;

            Random rnd = new Random();
            var randAmount = rnd.Next(1, 500);

            //act
            bankServiceManager.Transfer(new TransferRequest
            {
                AccountId = originAccountId,
                TargetAccountId = destinationAccountId,
                Amount = randAmount
            });

            var originAccount = bankServiceManager.ListAccounts().Find(ta => ta.AccountId == originAccountId);
            var destinationAccount = bankServiceManager.ListAccounts().Find(ta => ta.AccountId == destinationAccountId);

            //assert
            Assert.AreEqual(Convert.ToDecimal(-randAmount), originAccount.Balance);
            Assert.AreEqual(1, originAccount.Transactions.Count);

            Assert.AreEqual(Convert.ToDecimal(randAmount), destinationAccount.Balance);
            Assert.AreEqual(1, destinationAccount.Transactions.Count);

        }

        [TestMethod]
        public void TransferAmountOver500OnInvestmentIndividual_Should_Fail() 
        {
            //arrange
             var bankServiceManager = CreateThreeBanksWithAllAccountTypesEach();

             var originAccountId = bankServiceManager.ListAccounts().First(a => a.AccountType == AccountType.InvestmentIndividual).AccountId; 
             var destinationAccountId =  bankServiceManager.ListAccounts().Last(a => a.AccountType != AccountType.InvestmentIndividual).AccountId;

            Random rnd = new Random();
            var randAmount = rnd.Next(501, 10000);

            //act
            try
            {
                bankServiceManager.Transfer(new TransferRequest
                {
                    AccountId = originAccountId,
                    TargetAccountId = destinationAccountId,
                    Amount = randAmount

                });
                Assert.Fail("Should Not Reach here");
            }
            catch (WithdrawLimitReachedException wlre)
            {
                var originAccount = bankServiceManager.ListAccounts().Find(ta => ta.AccountId == originAccountId);
                var destinationAccount = bankServiceManager.ListAccounts().Find(ta => ta.AccountId == destinationAccountId);

                //assert
                Assert.AreEqual(Convert.ToDecimal(0), originAccount.Balance);
                Assert.AreEqual(0, originAccount.Transactions.Count);

                Assert.AreEqual(Convert.ToDecimal(0), destinationAccount.Balance);
                Assert.AreEqual(0, destinationAccount.Transactions.Count);                                 
            }
            catch(Exception ex) 
            {
                //assert
                Assert.Fail("Incorrect exception thrown");
            }

        }
    
    } 
}