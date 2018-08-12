using System;
using System.Collections.Generic;

namespace Xn.Platform.Application.Core
{
    [Serializable]
    public class BatchConfig
    {
        public const string MySqlDateFormat = "yyyy-MM-dd";
        public const string MySqlTimeFormat = "yyyy-MM-dd HH:mm:00";

        private readonly IDictionary<string, string> _daysParameters = new Dictionary<string, string>();
        private readonly IDictionary<string, string> _parameters = new Dictionary<string, string>();

        public void SetSchedulerTime(DateTime schedulerTime)
        {
            SchedulerTime = schedulerTime;
        }

        public IDictionary<string, string> GetDateParameters(int days)
        {
            _daysParameters.Clear();
            _daysParameters["day"] = SchedulerDate.AddDays(days).ToString(MySqlDateFormat);
            return _daysParameters;
        }

        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public DateTime LastSchedulerTime { get; set; }
        public DateTime SchedulerTime { get; private set; }

        public DateTime SchedulerDate
        {
            get { return SchedulerTime.Date; }
        }

        public IDictionary<string, string> DateParameters
        {
            get
            {
                _parameters.Clear();
                _parameters["day"] = SchedulerDate.ToString(MySqlDateFormat);
                return _parameters;
            }
        }

        public IDictionary<string, string> NextDateParameters
        {
            get
            {
                _parameters.Clear();
                _parameters["day"] = SchedulerDate.AddMonths(-1).ToString(MySqlDateFormat);     //上个月
                return _parameters;
            }
        }


        public IDictionary<string, string> TimeParameters
        {
            get
            {
                _parameters.Clear();
                _parameters["day"] = SchedulerTime.ToString(MySqlTimeFormat);
                return _parameters;
            }
        }
    }
}
