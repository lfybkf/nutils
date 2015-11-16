using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taimer
{
	public class Taim
	{
		public static Timer timerMain;
		public static Timer timerSec;

		public Taim(int aMin)
		{
			Min = aMin;
		}//constructor

		const int mMillisec = 1000 * 60;
		const string fmtChas = "{0} час";
		const string fmtChasMin = "{0} час {1} мин";
		const string fmtMin = "{0} мин";

		public int Min = 1;
		public int Millisec { get { return Min * mMillisec; } }
		DateTime dtStarted = DateTime.MinValue;
		DateTime dtEnd = DateTime.MaxValue;
		
		public override string ToString()
		{
			if (Min < 60) { return string.Format(fmtMin, Min); }
			int Chas = Min / 60;
			int Mun = Min % 60;
			if (Mun == 0) { return string.Format(fmtChas, Chas); }
			else { return string.Format(fmtChasMin, Chas, Mun); }
		}//function

		public void Start()
		{
			Stop();

			dtStarted = DateTime.Now;
			dtEnd = dtStarted.AddMinutes(Min);
			timerMain.Interval = Millisec;
			timerMain.Start();
			timerSec.Start();
		}//function

		public void Stop()
		{
			dtStarted = DateTime.MinValue;
			dtEnd = DateTime.MaxValue;
			timerMain.Interval = Millisec;
			timerMain.Stop();
			timerSec.Stop();
		}//function

		public bool IsWorking { get { return dtStarted != DateTime.MinValue; } }

		public int MinRest { get {
			if (IsWorking)
			{
				TimeSpan ts = dtEnd.Subtract(DateTime.Now);
				return ts.Minutes ;
			}//if
			return 0;
		} }

		const string fmtInfo = "Таймер \t{0} мин\nОсталось \t{1} мин\nСтарт \t{2}\nФинал \t{3}";
		public string Info { get {
			return string.Format(fmtInfo, Min, MinRest, dtStarted.ToShortTimeString(), dtEnd.ToShortTimeString());
		}}
	}//class
}//namespace
