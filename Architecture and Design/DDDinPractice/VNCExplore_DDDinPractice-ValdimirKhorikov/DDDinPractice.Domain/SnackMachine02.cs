using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static DDDinPractice.Domain.Money02;

namespace DDDinPractice.Domain
{
    public sealed class SnackMachine02
    {
        // Money in machine

        public Money02 MoneyInside { get; private set; } = None;

        //public int OneCentCount { get; private set; }
        //public int FiveCentCount { get; private set; }
        //public int TenCentCount { get; private set; }
        //public int QuarterCount { get; private set; }
        //public int OneDollarCount { get; private set; }
        //public int FiveDollarCount { get; private set; }
        //public int TenDollarCount { get; private set; }
        //public int TwentyDollarCount { get; private set; }

        // Money in transaction

        public Money02 MoneyInTransaction { get; private set; } = None;

        //public int OneCentCountInTransaction { get; private set; }
        //public int FiveCentCountInTransaction { get; private set; }
        //public int TenCentCountInTransaction { get; private set; }
        //public int QuarterCountInTransaction { get; private set; }
        //public int OneDollarCountInTransaction { get; private set; }
        //public int FiveDollarCountInTransaction { get; private set; }
        //public int TenDollarCountInTransaction { get; private set; }
        //public int TwentyDollarCountInTransaction { get; private set; }

        public void InsertMoney(Money02 money)
        {
            MoneyInTransaction += money;
        }

        //public void InsertMoney(
        //    int oneCentCount,
        //    int fiveCentCount,
        //    int tenCentCount,
        //    int quarterCount,
        //    int oneDollarCount,
        //    int fiveDollarCount,
        //    int tenDollarCount,
        //    int twentyDollarCount)
        //{
        //    OneCentCountInTransaction += oneCentCount;
        //    FiveCentCountInTransaction += fiveCentCount;
        //    TenCentCountInTransaction += tenCentCount;
        //    QuarterCountInTransaction += quarterCount;
        //    OneDollarCountInTransaction += oneDollarCount;
        //    FiveDollarCountInTransaction += fiveDollarCount;
        //    TenDollarCountInTransaction += tenDollarCount;
        //    TwentyDollarCountInTransaction += twentyDollarCount;
        //}

        public void ReturnMoney()
        {
            MoneyInTransaction = None;
        }

        //public void ReturnMoney()
        //{
        //    OneCentCountInTransaction = 0;
        //    FiveCentCountInTransaction = 0;
        //    TenCentCountInTransaction = 0;
        //    QuarterCountInTransaction = 0;
        //    OneDollarCountInTransaction = 0;
        //    FiveDollarCountInTransaction = 0;
        //    TenDollarCountInTransaction = 0;
        //    TwentyDollarCountInTransaction = 0;
        //}

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }

        //public void BuySnack()
        //{
        //    OneCentCount += OneCentCountInTransaction;
        //    FiveCentCount += FiveCentCountInTransaction;
        //    TenCentCount += TenCentCountInTransaction;
        //    QuarterCount += QuarterCountInTransaction;
        //    OneDollarCount += OneDollarCountInTransaction;
        //    FiveDollarCount += FiveDollarCountInTransaction;
        //    TenDollarCount += TenDollarCountInTransaction;
        //    TwentyDollarCount += TwentyDollarCountInTransaction;

        //    OneCentCountInTransaction = 0;
        //    FiveCentCountInTransaction = 0;
        //    QuarterCountInTransaction = 0;
        //    OneDollarCountInTransaction = 0;
        //    FiveDollarCountInTransaction = 0;
        //    TenDollarCountInTransaction = 0;
        //    TwentyDollarCountInTransaction = 0;
        //}
    }
}
