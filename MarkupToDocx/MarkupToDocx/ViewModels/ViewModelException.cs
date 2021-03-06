using System;

namespace MarkupToDocx.ViewModels
{
    public class ViewModelException : Exception
    {
        public ViewModelException(string message)
            : base(message)
        {
        }

        public ViewModelException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}