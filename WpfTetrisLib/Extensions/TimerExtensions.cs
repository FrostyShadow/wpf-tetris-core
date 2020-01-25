using System;
using System.Reactive.Linq;
using System.Timers;

namespace WpfTetrisLib.Extensions
{
    public static class TimerExtensions
    {
        /// <summary>
        /// Gets ElapsedEvent as IObservable
        /// </summary>
        /// <param name="self">Timer</param>
        /// <returns>ElapsedEvent as IObservable</returns>
        public static IObservable<ElapsedEventArgs> ElapsedAsObservable(this Timer self)
        {
            if(self == null)
                throw new ArgumentNullException(nameof(self));

            return Observable.FromEvent<ElapsedEventHandler, ElapsedEventArgs>(
                h => (s, e) => h(e),
                h => self.Elapsed += h,
                h => self.Elapsed -= h);
        }
    }
}