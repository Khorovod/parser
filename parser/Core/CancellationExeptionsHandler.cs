using System;


namespace parser.Core
{
    static class CancellationExeptionsHandler
    {
        public static string Execute(Action action)
        {
            string res = null;
            try
            {
                action.Invoke();
            }
            catch (OperationCanceledException ex)
            {
                res = ex.Message;
            }
            return res;
        }
    }
}
