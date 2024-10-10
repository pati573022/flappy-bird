namespace flappy_bird;

public partial class MainPage : ContentPage
{
	
const int gravidade = 40;
const int tempoEntreFrames =1000;
bool estaMorto = false;
	public MainPage()
	{
		InitializeComponent();
		
	}


	void AplicaGravidade()
	{
		passaro.TranslationY += gravidade;
	}
	
	async Task Desenhar ()
	{
      while (! estaMorto)
	  {
		 AplicaGravidade();
		 await Task.Delay(tempoEntreFrames);
	  }
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		Desenhar();
    }

}

