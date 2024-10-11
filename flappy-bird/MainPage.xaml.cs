namespace flappy_bird;

public partial class MainPage : ContentPage
{
	
const int gravidade = 8;
const int tempoEntreFrames = 25;
bool estaMorto = true;
double larguraJanela=0;
double alturaJanela=0;
int velocidade=10;
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
		 GerenciaCanos();
	  }
	}
   
    protected override void OnSizeAllocated(double w, double h)
    {
    base.OnSizeAllocated(w, h);
	larguraJanela=w;
	alturaJanela=h;
    }

	void GerenciaCanos()
	{
		imgCanocima.TranslationX-= velocidade;
		imgCanobaixo.TranslationX-= velocidade;
		if(imgCanobaixo.TranslationX<=-larguraJanela)
		{
			imgCanobaixo.TranslationX=0;
			imgCanocima.TranslationX=0;
		}

	}

	void OnGameOverClicked(object s,TappedEventArgs a)
	{
		frameGameOver.IsVisible=false;
		Inicializar();
		Desenhar();
	}

	void Inicializar()
	{
		estaMorto=false;
		passaro.TranslationY=0;
	}

}

