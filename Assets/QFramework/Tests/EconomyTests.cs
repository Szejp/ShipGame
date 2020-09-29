//using System.Collections.Generic;
//using NUnit.Framework;
//using QFramework.Modules.QEconomy;
//using QFramework.Modules.QEconomy.Api;
//
//namespace Tests
//{
//    public class EconomyTests
//    {
//        const string SOFT_CURRENCY_NAME = "soft_currency";
//        const string HARD_CURRENCY_NAME = "hard_currency";
//
//        EconomyData EconomyData;
//
//        [SetUp]
//        public void Setup()
//        {
//            EconomyData = new EconomyData();
//            EconomyData.currencies = new List<Currency>()
//            {
//                new Currency(SOFT_CURRENCY_NAME, 10),
//                new Currency(HARD_CURRENCY_NAME, 0)
//            };
//
//            Economy.Init(EconomyData);
//        }
//
//        // A Test behaves as an ordinary method
//        [Test]
//        public void EconomyTestsAddCurrency()
//        {
//            var valueToAdd = 200;
//            var savedSoftCurrencyAmount = Economy.GetCurrency(SOFT_CURRENCY_NAME).Amount;
//            Economy.AddCurrency(new Currency(SOFT_CURRENCY_NAME, valueToAdd));
//            Assert.AreEqual(Economy.GetCurrency(SOFT_CURRENCY_NAME).Amount, savedSoftCurrencyAmount + valueToAdd);
//
//            var savedHardCurrencyAmount = Economy.GetCurrency(HARD_CURRENCY_NAME).Amount;
//            Economy.AddCurrency(new Currency(HARD_CURRENCY_NAME, valueToAdd));
//            Assert.AreEqual(Economy.GetCurrency(HARD_CURRENCY_NAME).Amount, savedHardCurrencyAmount + valueToAdd);
//        }
//
//        [Test]
//        public void EconomyTestsRemoveCurrency()
//        {
//            var valueToRemove = 10;
//            var savedCurrencyAmount = Economy.GetCurrency(SOFT_CURRENCY_NAME).Amount;
//            Economy.AddCurrency(new Currency(SOFT_CURRENCY_NAME, -valueToRemove));
//            Assert.AreEqual(Economy.GetCurrency(SOFT_CURRENCY_NAME).Amount, savedCurrencyAmount - valueToRemove);
//        }
//
//        [Test]
//        public void EconomyTestsNotBelowZeroCurrency()
//        {
//            Economy.AddCurrency(new Currency(SOFT_CURRENCY_NAME, -100));
//            Assert.AreEqual(Economy.GetCurrency(SOFT_CURRENCY_NAME).Amount, 0);
//        }
//
//        [Test]
//        public void ModifyCurrencyFireCallbackTest()
//        {
//            var modifyValue = 225;
//            var eventsFiredCount = 0;
//            var modifiesCount = 5;
//            var hardCurrencyAmount = Economy.GetCurrency(CurrencyType.hard_currency).Amount;
//
//            Economy.OnCurrencyChanged += currency =>
//            {
//                if (currency.Id.Equals(HARD_CURRENCY_NAME))
//                    eventsFiredCount++;
//
//                Assert.AreEqual(currency.Amount, hardCurrencyAmount + modifyValue * eventsFiredCount);
//            };
//
//            for (int i = 1; i <= modifiesCount; i++)
//            {
//                Economy.ModifyCurrency(CurrencyType.hard_currency, modifyValue);
//                Assert.AreEqual(i,eventsFiredCount);
//            }
//
//            Assert.AreEqual(eventsFiredCount, modifiesCount);
//        }
//    }
//}