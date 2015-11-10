using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetriNetEditor;

namespace PetriNetEditorTests
{
    public class StubCommand : ICustomCommand
    {
        public bool CanExecuteChanged { get; set; }
        public CommandTypes CommandType { get; set; }

        public Predicate<dynamic> CanExecutePred { get; set; }

        public Action<dynamic> ExecuteActionOne { get; set; }

        public Action<dynamic, dynamic> ExecuteActionTwo { get; set; }

        public Action<dynamic, dynamic, dynamic> ExecuteActionThree { get; set; }

        public StubCommand(CommandTypes commandType)
        {
            CommandType = commandType;
            CanExecuteChanged = false;
        }

        public bool CanExecute(object parameter)
        {
            if (CanExecutePred == null)
                return true;
            return CanExecutePred(parameter);
        }

        public void Execute(object parameter)
        {
            if (ExecuteActionOne != null)
                ExecuteActionOne(parameter);
            else if (ExecuteActionTwo != null)
            {
                object[] paramArray = (object[])parameter;
                ExecuteActionTwo(paramArray[0], paramArray[1]);
            }
            else if (ExecuteActionThree != null)
            {
                object[] paramArray = (object[])parameter;
                ExecuteActionThree(paramArray[0], paramArray[1], paramArray[2]);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged = true;
        }
    }
}
