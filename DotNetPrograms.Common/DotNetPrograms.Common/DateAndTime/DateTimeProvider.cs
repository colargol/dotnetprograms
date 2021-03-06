﻿using System;

namespace DotNetPrograms.Common.DateAndTime
{
    public static class DateTimeProvider
    {
        static DateTimeProvider()
        {
            Reset();
        }

        private static Func<DateTime> _now;
        public static DateTime Now
        {
            get { return _now(); }
            set { _now = () => value; }
        }

        public static DateTime ReasonableMinValue
        {
            get { return new DateTime(1900, 1, 1); }
        }

        public static void Reset()
        {
            _now = () => DateTime.Now;
        }
    }
}