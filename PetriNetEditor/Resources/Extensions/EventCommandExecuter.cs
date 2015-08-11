using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PetriNetEditor
{
    /// <summary>
    /// This class represents an attached TriggerAction that allows for attaching a command to an event,
    /// converting the event data and using the converted data as the command parameter.
    /// </summary>
    public class EventCommandExecuter : TriggerAction<DependencyObject>
    {
        /// <summary> Gets or sets the EventArgsConverter to be used by this EventCommandExecuter. </summary>
        public IValueConverter EventArgsConverter { get; set; }

        /// <summary> Gets or sets the Culture to be used by this EventCommandExecuter. </summary>
        public CultureInfo Culture { get; set; }

        /// <summary> Gets or sets the command to be used by this EventCommandExecuter. </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary> The Command DependencyProperty. </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
            "Command",
            typeof(ICommand),
            typeof(EventCommandExecuter),
            new PropertyMetadata(null));

        /// <summary> 
        /// Gets or sets the parameter to be used by the EventArgsConverter of this EventCommandExecuter. 
        /// </summary>
        public object EventArgsConverterParameter
        {
            get { return (object)GetValue(EventArgsConverterParameterProperty); }
            set { SetValue(EventArgsConverterParameterProperty, value); }
        }

        /// <summary> The EventArgsConverterParameter DependencyProperty. </summary>
        public static readonly DependencyProperty EventArgsConverterParameterProperty =
            DependencyProperty.Register(
            "EventArgsConverterParameter",
            typeof(object),
            typeof(EventCommandExecuter),
            new PropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the EventCommandExecuter class with the current culture.
        /// </summary>
        public EventCommandExecuter() : this(CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the EventCommandExecuter class with the provided culture.
        /// </summary>
        /// <param name="culture">The culture to be used for the EventCommandExecuter instance.</param>
        public EventCommandExecuter(CultureInfo culture)
        {
            Culture = culture;
        }

        /// <summary>
        /// Invokes the command of this EventCommandExecuter with the converted event data as the command
        /// parameter.
        /// </summary>
        /// <param name="parameter">The raw event data.</param>
        protected override void Invoke(object parameter)
        {
            var cmd = Command;

            if (cmd != null)
            {
                var param = parameter;

                if (EventArgsConverter != null)
                {
                    param = EventArgsConverter.Convert(parameter, typeof(object), EventArgsConverterParameter, 
                                                       CultureInfo.InvariantCulture);
                }

                if (cmd.CanExecute(param))
                {
                    cmd.Execute(param);
                }
            }
        }
    }
}
