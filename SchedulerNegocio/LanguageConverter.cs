using System;
using System.Collections.Generic;
using System.Globalization;


namespace SchedulerNegocio
{
    public class LanguageConverter
    {

        private CultureInfo cultureinfo;
        private readonly Language languageSuported;

        public LanguageConverter(Language soportlanguage)
        {
            languageSuported = soportlanguage;
            cultureinfo = new CultureInfo(
                  Enum.GetName(typeof(Language), soportlanguage).ToString());
        }

        public Dictionary<string, string> TextTraductions
        {
            get
            {
                Dictionary<string, string> listTraductions = new();
                switch (languageSuported)
                {
                    case Language.ES:
                        listTraductions.Add("Configuration must be active", "La configuración debe estar activa");
                        listTraductions.Add("The type is not recognized", "No se reconoce el tipo");
                        listTraductions.Add("Configuration date can not be empty", "La fecha de configuración no puede estar vacía");
                        listTraductions.Add("Next execution time exceeds date limits", "La siguiente ejecución supera los límites de fecha");
                        listTraductions.Add("Current date can not be empty", "La fecha actual no puede estar vacía");
                        listTraductions.Add("The frequency is not set correctly", "La frecuencia no está configurada correctamente");
                        listTraductions.Add("Must be indicate the daily time", "Se debe indicar la hora diaria");
                        listTraductions.Add("Must to select a daily frequency type 'Once' or 'Every'", "Debe seleccionar un tipo de frecuencia diaria 'Una vez' o 'Cada'");
                        listTraductions.Add("The hourly frequency must be greater than 0", "La frecuencia horaria debe ser mayor que 0");
                        listTraductions.Add("Must indicate the type of frecuency Hours, Minutes o Seconds correctly", "Debe indicar el tipo de frecuencia Horas, Minutos o Segundos correctamente");
                        listTraductions.Add("The Horary Range is not set correctly", "El rango horario no está configurado correctamente");
                        listTraductions.Add("The weekly frequency must be greater than 0", "La frecuencia semanal debe ser mayor a 0");
                        listTraductions.Add("You must select at least one day of the week", "Debe seleccionar al menos un día de la semana");
                        listTraductions.Add("You must select First, Second, Third or Last", "Debes seleccionar Primero, Segundo, Tercero o Último");
                        listTraductions.Add("Ocurrs once. ", "Ocurre una vez. ");
                        listTraductions.Add("Schedule will be used on", "Se utilizará el horario el ");
                        listTraductions.Add(" starting on ", " a partir de ");
                        listTraductions.Add("Ocurrs every day. ", "Ocurre todos los días. ");
                        listTraductions.Add("Ocurrs every ", "Ocurre cada ");
                        listTraductions.Add(" weeks on ", " semanas en ");
                        listTraductions.Add("Ocurrs the ", "Ocurre el ");
                        listTraductions.Add(" of every ", " de cada ");
                        listTraductions.Add(" months. ", " meses. ");
                        listTraductions.Add(" hours ", " horas ");
                        listTraductions.Add(" minutes ", " minutos ");
                        listTraductions.Add(" seconds ", " segundos ");
                        listTraductions.Add(" every ", " cada ");
                        listTraductions.Add("between ", "entre ");
                        listTraductions.Add(" and ", " y ");
                        listTraductions.Add("Start date can not be empty", "La fecha de inicio no puede estar vacía");
                        listTraductions.Add("End date cant not be equal start date", "La fecha de finalización no puede ser igual a la fecha de inicio");
                        listTraductions.Add("Monday", "Lunes");
                        listTraductions.Add("Tuesday", "Martes");
                        listTraductions.Add("Wednesday", "Miércoles");
                        listTraductions.Add("Thursday", "Jueves");
                        listTraductions.Add("Friday", "Viernes");
                        listTraductions.Add("Saturday", "Sábado");
                        listTraductions.Add("Sunday", "Domingo");
                        listTraductions.Add("First", "primer");
                        listTraductions.Add("Second", "segundo");
                        listTraductions.Add("Third", "tercero");
                        listTraductions.Add("Fourth", "cuarto");
                        listTraductions.Add("Last", "último");
                        listTraductions.Add("day", "dia");
                        listTraductions.Add("weekday", "rango de días laborales entre semana del mes");
                        listTraductions.Add("weekendday", "fin de semana");
                        break;
                    case Language.US:
                    case Language.UK:
                    default:
                        listTraductions.Add("Configuration must be active", "Configuration must be active");
                        listTraductions.Add("The type is not recognized", "The type is not recognized");
                        listTraductions.Add("Configuration date can not be empty", "Configuration date can not be empty");
                        listTraductions.Add("Next execution time exceeds date limits", "Next execution time exceeds date limits");
                        listTraductions.Add("Current date can not be empty", "Current date can not be empty");
                        listTraductions.Add("The frequency is not set correctly", "The frequency is not set correctly");
                        listTraductions.Add("Must be indicate the daily time", "Must be indicate the daily time");
                        listTraductions.Add("Must to select a daily frequency type 'Once' or 'Every'", "Must to select a daily frequency type 'Once' or 'Every'");
                        listTraductions.Add("The hourly frequency must be greater than 0", "The hourly frequency must be greater than 0");
                        listTraductions.Add("Must indicate the type of frecuency Hours, Minutes o Seconds correctly", "Must indicate the type of frecuency Hours, Minutes o Seconds correctly");
                        listTraductions.Add("The Horary Range is not set correctly", "The Horary Range is not set correctly");
                        listTraductions.Add("The weekly frequency must be greater than 0", "The weekly frequency must be greater than 0");
                        listTraductions.Add("You must select at least one day of the week", "You must select at least one day of the week");
                        listTraductions.Add("You must select First, Second, Third or Last", "You must select First, Second, Third or Last");
                        listTraductions.Add("Ocurrs once. ", "Ocurrs once. ");
                        listTraductions.Add("Schedule will be used on", "Schedule will be used on ");
                        listTraductions.Add(" starting on ", " starting on ");
                        listTraductions.Add("Ocurrs every day. ", "Ocurrs every day. ");
                        listTraductions.Add("Ocurrs every ", "Ocurrs every ");
                        listTraductions.Add(" weeks on ", " weeks on ");
                        listTraductions.Add("Ocurrs the ", "Ocurrs the ");
                        listTraductions.Add(" of every ", " of every ");
                        listTraductions.Add(" months. ", " months. ");
                        listTraductions.Add(" hours ", " hours ");
                        listTraductions.Add(" minutes ", " minutes ");
                        listTraductions.Add(" seconds ", " seconds ");
                        listTraductions.Add(" every ", " every ");
                        listTraductions.Add("between ", "between ");
                        listTraductions.Add(" and ", " and ");
                        listTraductions.Add("Start date can not be empty", "Start date can not be empty");
                        listTraductions.Add("End date cant not be equal start date", "End date cant not be equal start date");
                        listTraductions.Add("Monday", "Monday");
                        listTraductions.Add("Tuesday", "Tuesday");
                        listTraductions.Add("Wednesday", "Wednesday");
                        listTraductions.Add("Thursday", "Thursday");
                        listTraductions.Add("Friday", "Friday");
                        listTraductions.Add("Saturday", "Saturday");
                        listTraductions.Add("Sunday", "Sunday");
                        listTraductions.Add("First", "First");
                        listTraductions.Add("Second", "Second");
                        listTraductions.Add("Third", "Third");
                        listTraductions.Add("Fourth", "Fourth");
                        listTraductions.Add("Last", "Last");
                        listTraductions.Add("day", "day");
                        listTraductions.Add("weekday", "weekday");
                        listTraductions.Add("weekendday", "weekendday");
                        break;
                }
                return listTraductions;
            }
        }

        public string GetTraduction(string text)
        {
            string traductionText = "";
            foreach (KeyValuePair<string, string> traduction in this.TextTraductions)
            {
                if (text == traduction.Key.ToString())
                {
                    traductionText = traduction.Value.ToString();
                }
            }
            return traductionText;
        }
        public List<string> GetTraductionList(List<DayOfWeek> text)
        {
            List<string> traductionList = new();
            foreach (KeyValuePair<string, string> traduction in this.TextTraductions)
            {
                foreach (DayOfWeek str in text)
                {
                    if (str.ToString() == traduction.Key.ToString())
                    {
                        traductionList.Add(traduction.Value.ToString());
                    }
                }
            }
            return traductionList;
        }

        public string GetDateTimeFormatedLanguage(DateTime date)
        {          
            return date.ToString("d", cultureinfo).Replace(".","/") + ' ' + date.TimeOfDay.ToString();
        }
        public string GetDateFormatedLanguage(DateTime date)
        {
            return date.ToString("d", cultureinfo).Replace(".", "/");
        }
    }


}
