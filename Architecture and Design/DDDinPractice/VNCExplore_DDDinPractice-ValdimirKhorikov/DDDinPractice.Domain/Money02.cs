using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDinPractice.Domain
{
    public sealed class Money02
    {
        public static readonly Money02 None = new Money02(0, 0, 0, 0, 0, 0, 0, 0);

        public static readonly Money02 Cent = new Money02(1, 0, 0, 0, 0, 0, 0, 0);
        public static readonly Money02 FiveCent = new Money02(0, 1, 0, 0, 0, 0, 0, 0);
        public static readonly Money02 TenCent = new Money02(0, 0, 1, 0, 0, 0, 0, 0);
        public static readonly Money02 Quarter = new Money02(0, 0, 0, 1, 0, 0, 0, 0);

        public static readonly Money02 Dollar = new Money02(0, 0, 0, 0, 1, 0, 0, 0);
        public static readonly Money02 FiveDollar = new Money02(0, 0, 0, 0, 0, 1, 0, 0);
        public static readonly Money02 TenDollar = new Money02(0, 0, 0, 0, 0, 0, 1, 0);
        public static readonly Money02 TwentyDollar = new Money02(0, 0, 0, 0, 0, 0, 0, 1);

        // Items in Money

        public int OneCentCount { get; private set; }
        public int FiveCentCount { get; private set; }
        public int TenCentCount { get; private set; }
        public int QuarterCount { get; private set; }
        public int OneDollarCount { get; private set; }
        public int FiveDollarCount { get; private set; }
        public int TenDollarCount { get; private set; }
        public int TwentyDollarCount { get; private set; }

        public Money02(
            int oneCentCount,
            int fiveCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int tenDollarCount,
            int twentyDollarCount)
        {
            if (oneCentCount < 0)
                throw new InvalidOperationException();
            if (fiveCentCount < 0)
                throw new InvalidOperationException();
            if (tenCentCount < 0)
                throw new InvalidOperationException();
            if (quarterCount < 0)
                throw new InvalidOperationException();
            if (oneDollarCount < 0)
                throw new InvalidOperationException();
            if (fiveDollarCount < 0)
                throw new InvalidOperationException();
            if (tenDollarCount < 0)
                throw new InvalidOperationException();
            if (twentyDollarCount < 0)
                throw new InvalidOperationException();

            OneCentCount += oneCentCount;
            FiveCentCount += fiveCentCount;
            TenCentCount += tenCentCount;
            QuarterCount += quarterCount;
            OneDollarCount += oneDollarCount;
            FiveDollarCount += fiveDollarCount;
            TenDollarCount += tenDollarCount;
            TwentyDollarCount += twentyDollarCount;
        }

        public static Money02 operator +(Money02 money1, Money02 money2)
        {
            Money02 sum = new Money02(
                money1.OneCentCount + money2.OneCentCount,
                money1.FiveCentCount + money2.FiveCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount,
                money1.TenDollarCount + money2.TenDollarCount,
                money1.TwentyDollarCount + money2.TwentyDollarCount);

            return sum;
        }

        public static Money02 operator -(Money02 money1, Money02 money2)
        {
            return new Money02(
                money1.OneCentCount - money2.OneCentCount,
                money1.FiveCentCount - money2.FiveCentCount,
                money1.TenCentCount - money2.TenCentCount,
                money1.QuarterCount - money2.QuarterCount,
                money1.OneDollarCount - money2.OneDollarCount,
                money1.FiveDollarCount - money2.FiveDollarCount,
                money1.TenDollarCount - money2.TenDollarCount,
                money1.TwentyDollarCount - money2.TwentyDollarCount);
        }
    }
}
