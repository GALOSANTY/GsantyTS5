using GsantyTS5.Models;
namespace GsantyTS5.Views;

public partial class vPersona : ContentPage
{
    public vPersona()
    {
        InitializeComponent();
    }
    private Persona selectedPersona;

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {

        if (selectedPersona == null)
        {
            Persona newPersona = new Persona
            {
                Name = txtPersona.Text,

            };
            lblStatus.Text = "";
            App.PersonRepo.AddNewPerson(newPersona);
            lblStatus.Text = App.PersonRepo.statusMessage;
            

        }
        else
        {
            selectedPersona.Name = txtPersona.Text;
            lblStatus.Text = "";
            App.PersonRepo.AddNewPerson(selectedPersona);
            lblStatus.Text = App.PersonRepo.statusMessage;
            selectedPersona = null; 

        }

    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        List<Persona> people = App.PersonRepo.GetAllPeople();
        Listapersonas.ItemsSource = people;
    }

    private async void btnEditar_Clicked(object sender, EventArgs e)
    {
        selectedPersona = (sender as Button).CommandParameter as Persona;
        txtPersona.Text = selectedPersona.Name;
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        
        selectedPersona = (sender as Button).CommandParameter as Persona;
        txtPersona.Text = selectedPersona.Name;

        if (selectedPersona != null)
        {

            int personIdToDelete = selectedPersona.Id;
            App.PersonRepo.DeletePerson(personIdToDelete);
            lblStatus.Text = App.PersonRepo.statusMessage;




        }
        else
        {
           
            await MostrarMensajeError("Debes seleccionar una persona de la lista");
        }
    }

    private async Task MostrarMensajeError(string mensaje)
    {
        await App.Current.MainPage.DisplayAlert("Error", mensaje, "Aceptar");
    }
}
