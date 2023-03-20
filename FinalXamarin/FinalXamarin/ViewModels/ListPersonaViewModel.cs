using FinalXamarin.Models;
using FinalXamarin.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FinalXamarin.ViewModels
{
    public class ListPersonaViewModel
    {
        public ICommand Exit => new Command(ButtonsPage);

        public async void ButtonsPage()
        {
            //nos envía a la pagina de registrar Personas
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

        public ICommand Delete => new Command(DeletePersona);
        public ICommand Ubicacion => new Command(UbicacionPersona);
        public ICommand Update => new Command(UpdatePersona);
        public PersonaModel SelectPersona { get; set; }
        public ObservableCollection<PersonaModel> ListPersonas { get; set; }

        //Metodo para eliminar un registro seleccionado de la lista
        public async void DeletePersona()
        {
            if (SelectPersona != null)
            {
                var res = await App.Current.MainPage.DisplayAlert("Alerta", "Desea eliminar el registro seleccionado", "si", "no");
                if (res)
                {
                    var del = App.Database.DeletePersonasAsync(SelectPersona);
                    await App.Current.MainPage.DisplayAlert("Alerta", "El registro ha sido eliminado con exito", "ok");
                    await App.Current.MainPage.Navigation.PushAsync(new PersonaListPage());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "El registro no ha sido eliminado", "ok");
                    await App.Current.MainPage.Navigation.PushAsync(new PersonaListPage());
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Seleccione el registro que de sea eliminar", "ok");
            }
        }

        public async void UbicacionPersona()
        {
            if (SelectPersona != null)
            {
                App.Current.Properties["id"] = SelectPersona.ID;
                App.Current.Properties["nom"] = SelectPersona.Nombre;
                App.Current.Properties["Tel"] = SelectPersona.Telefono;
                App.Current.Properties["Direc"] = SelectPersona.Direccion;
                await App.Current.MainPage.Navigation.PushAsync(new UbicacionPage());
                //nos elimina la pagina anterior del estack de paginas
                var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
                foreach (var page in existingPages)
                {
                    if (page.GetType() == typeof(UbicacionPage))
                        continue;
                    App.Current.MainPage.Navigation.RemovePage(page);
                }


            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Seleccione la direccion " +
                    "se mostrara en el mapa", "ok");
            }
            
        }
        //Método para editar o actualizar un registro de la lista
        public async void UpdatePersona()
        {
            if (SelectPersona != null)
            {
                App.Current.Properties["id"] = SelectPersona.ID;
                App.Current.Properties["nom"] = SelectPersona.Nombre;
                App.Current.Properties["Tel"] = SelectPersona.Telefono;
                App.Current.Properties["Direc"] = SelectPersona.Direccion;
                await App.Current.MainPage.Navigation.PushAsync(new UpdPersonaPage());


            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Seleccione el " +
                    "registro que de sea modificar", "ok");
            }
        }

        //Metodo para llenar la lista de la página
        //creador: El patrón creador nos dice quien es responsable de la creación o quien instancia nuevos objetos y/o clases.
        public ListPersonaViewModel()
        {
            //que se encarga de llenar la lista de personas en la página.
            FillList();
        }
        private async void FillList()
        {
            //Dentro de este método, se crea una instancia -llama-
            ListPersonas = new ObservableCollection<PersonaModel>();
            // Luego, se llama al método
            //para obtener la lista de personas de la base de datos.
            var MyList = await App.Database.GetPersonasAsync();
            foreach (var item in MyList)
            {
                //crear una nueva instancia
                // y agregarla a
                ListPersonas.Add(new PersonaModel
                {
                    ID = item.ID,
                    Nombre = item.Nombre,
                    Telefono = item.Telefono,
                    Direccion = item.Direccion,
                });
            }
        }
    }
}
