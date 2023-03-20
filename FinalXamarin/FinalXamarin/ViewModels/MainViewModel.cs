using FinalXamarin.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalXamarin.ViewModels
{
    public class MainViewModel
    {
        public ICommand RegisterPersona => new Command(ShowRegisterPersonaPage);
        public ICommand ListPersonas => new Command(ShowListPersonasPage);

        public async void ShowRegisterPersonaPage()
        {
            //nos envía a la pagina del listado persona
            await App.Current.MainPage.Navigation.PushAsync(new RegisterPersonaPage());
            //nos elimina la pagina anterior del estack de paginas
            var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(RegisterPersonaPage))
                    continue;
                App.Current.MainPage.Navigation.RemovePage(page);
            }

        }
        public async void ShowListPersonasPage()
        {
            //nos envía a la pagina del listado personas
            await App.Current.MainPage.Navigation.PushAsync(new PersonaListPage());
            //nos elimina la pagina anterior del estack de paginas
            var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(PersonaListPage))
                    continue;
                App.Current.MainPage.Navigation.RemovePage(page);
            }

        }

    }
}
