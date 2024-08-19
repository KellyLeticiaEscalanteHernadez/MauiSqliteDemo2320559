
namespace MauiSqliteDemo2320559
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editClientesId;
        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listview.ItemsSource = await _dbService.GetClientes());
        }
        private void OnCounterClicked(object sender, EventArgs e)
        {

        }
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (_editClientesId == 0)
            {
                //agrega cliente
                await _dbService.Create(new Cliente
                {
                    NombreCliente = nombreEntryField.Text,
                    Email = emailEntryField.Text,
                    Movil = movilEntryField.Text
                });
            }
            else
            {
                //ediat cliente
                await _dbService.Update(new Cliente
                {
                    Id = _editClientesId,   
                    NombreCliente= nombreEntryField.Text,   
                    Email = emailEntryField.Text,   
                    Movil = movilEntryField.Text    
                });
                _editClientesId = 0;
            }
            nombreEntryField.Text= string.Empty;    
            emailEntryField .Text= string.Empty;    
            movilEntryField .Text= string.Empty;

            listview.ItemsSource= await _dbService.GetClientes();   
        }
        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var cliente =(Cliente)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch(action) 
            {
                case "Edit":
                    _editClientesId= cliente.Id;
                    nombreEntryField .Text= cliente.NombreCliente;
                    emailEntryField.Text= cliente.Email;
                    movilEntryField.Text = cliente.Movil;   
                    listview.ItemsSource = await _dbService.GetClientes();    
                    break;

                case "Delete":
                    await _dbService.Delete(cliente);
                    listview.ItemsSource = await _dbService.GetClientes();
                    break;
            }
        }
    }
}
