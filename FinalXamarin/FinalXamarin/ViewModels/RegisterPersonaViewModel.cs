using FinalXamarin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalXamarin.ViewModels
{
    public class RegisterPersonaViewModel
    {
        public ICommand Exit => new Command(ButtonsPage);

        public async void ButtonsPage()
        {
            //nos envía a la pagina de registrar alumnos
            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            //nos elimina la pagina anterior del estack de paginas
            var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(MainPage))
                    continue;
                App.Current.MainPage.Navigation.RemovePage(page);
            }

        }
        public ICommand Save => new Command(SaveRegister);
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public async void SaveRegister()
        {
            if (Nombre != null)
            {
                if (Telefono != null)
                {
                    if (Direccion != null)
                    {
                        var Persona = new PersonaModel()
                    {
                        Nombre = Nombre,
                        Telefono = Telefono,
                        Direccion = Direccion,
                    };
                    var Sav = await App.Database.SavePersonasAsync(Persona);
                    if (Sav != null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "El registro fue guardado con exito", "ok");
                        await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                        //nos elimina la pagina anterior del estack de paginas
                        var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
                        foreach (var page in existingPages)
                        {
                            if (page.GetType() == typeof(MainPage))
                                continue;
                            App.Current.MainPage.Navigation.RemovePage(page);
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "El registro no fue guardado", "ok");
                    
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "Debe ingresar datos direccion", "ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "Debe ingresar datos telefono", "ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Debe ingrasar nombre para continuar", "ok");
            }

        }
    }
}
