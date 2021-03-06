using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using HourGlass.Commands;
using HourGlass.Lib.Domain;
using HourGlass.Lib.Services;
using HourGlass.Providers;

namespace HourGlass.ViewModels
{
    public class WeekViewModel : ViewModelBase
    {
        public ICommand SaveWeekCommand { get; private set; }
        public ICommand AddUsageCommand { get; private set; }
        public ObservableCollection<HourUsageViewModel> Usages { get; set; }

        public Week Week{ get; private set;}
        private readonly IWeekService _weekService;
        private readonly IHourCodeProvider _hourCodeProvider;

        public WeekViewModel(IWeekService weekService, IHourCodeProvider hourCodeProvider, Week week)
        {
            _weekService = weekService;
            Week = week;
            _hourCodeProvider = hourCodeProvider;

            Usages = new ObservableCollection<HourUsageViewModel>();
            foreach (var hourUsage in Week.Usages)
            {
                Usages.Add(new HourUsageViewModel(_hourCodeProvider, this, hourUsage));
            }
            SaveWeekCommand = new DelegateCommand(SaveWeek);
            AddUsageCommand = new DelegateCommand(AddUsage);
        }

        private void AddUsage(object parameter)
        {
            var usage = new HourUsage();
            Week.AddUsage(usage);
            Usages.Add(new HourUsageViewModel(_hourCodeProvider, this, usage));
        }

        public void Remove(HourUsageViewModel hourUsageViewModel)
        {
            Week.RemoveUsage(hourUsageViewModel.Usage);
            Usages.Remove(hourUsageViewModel);
            NumbersChanged();
        }

        private void SaveWeek(object parameter)
        {
            _weekService.Save(Week);
        }
        
        public void NumbersChanged()
        {
            OnPropertiesChanged("Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", "Sum", "Name");
        }

        public DateTime StartDate
        {
            get { return Week.StartDate; }
            set
            {
                var day = value;
                while (day.DayOfWeek != DayOfWeek.Monday)
                {
                    day = day.AddDays(-1);
                }
                Week.StartDate = day;
                OnPropertiesChanged("StartDate", "Year", "Number", "Name");
                OnPropertiesChanged("MondayName", "TuesdayName", "WednesdayName", "ThursdayName", "FridayName", "SaturdayName", "SundayName");
            }
        }

        public int Year
        {
            get { return Week.Year; }
        }

        public int Number
        {
            get { return Week.Number; }
        }

        public string MondayName
        {
            get
            {
                return DayName("Mon", 0);
            }
        }

        public string TuesdayName
        {
            get
            {
                return DayName("Tue", 1);
            }
        }

        public string WednesdayName
        {
            get
            {
                return DayName("Wed", 2);
            }
        }

        public string ThursdayName
        {
            get
            {
                return DayName("Thu", 3);
            }
        }

        public string FridayName
        {
            get
            {
                return DayName("Fri", 4);
            }
        }

        public string SaturdayName
        {
            get
            {
                return DayName("Sat", 5);
            }
        }

        public string SundayName
        {
            get
            {
                return DayName("Sun", 6);
            }
        }

        private string DayName(string day, int dayNumber)
        {
            var date = StartDate.AddDays(dayNumber);
            return new StringBuilder()
                .AppendLine(day)
                .Append(date.Day).Append(".").Append(date.Month)
                .ToString();
        }

        public double Monday
        {
            get { return Week.Monday; }
        }

        public double Tuesday
        {
            get { return Week.Tuesday; }
        }

        public double Wednesday
        {
            get { return Week.Wednesday; }
        }

        public double Thursday
        {
            get { return Week.Thursday; }
        }

        public double Friday
        {
            get { return Week.Friday; }
        }

        public double Saturday
        {
            get { return Week.Saturday; }
        }

        public double Sunday
        {
            get { return Week.Sunday; }
        }

        public double Sum
        {
            get { return Week.Sum; }
        }

        public string Name
        {
            get
            {
                return new StringBuilder()
                .Append(Year)
                .Append(" - ")
                .Append(Number)
                .Append(" (").Append(Sum).Append(")").ToString();
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}