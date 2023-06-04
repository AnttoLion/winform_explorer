using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mjc_dev.common
{
    public class NavigationHistory
    {
        private Stack<Form> historyStack;

        public NavigationHistory()
        {
            historyStack = new Stack<Form>();
        }

        public int Count
        {
            get { return historyStack.Count; }
        }

        public void Push(Form form)
        {
            historyStack.Push(form);
        }

        public Form Pop()
        {
            return historyStack.Pop();
        }
    }
}
