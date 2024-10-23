using System.Threading.Channels;
using Windows.ApplicationModel.Search.Core;
using Windows.UI.Input;

namespace flappy_bird;

public partial class MainPage : ContentPage
{

	const int gravidade = 8;
	const int tempoEntreFrames = 25;
	bool estaMorto = true;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 10;
	const int maxTempoPulando = 2;
	int tempoPulando = 0;
	bool estaPulando = false;
	const int forcaPulo = 40;
	const int aberturaMin = 100;
	int score = 0;
	public MainPage()
	{
		InitializeComponent();

	}


	void AplicaGravidade()
	{
		passaro.TranslationY += gravidade;
	}
	void AplicaPulo()
	{
		passaro.TranslationY -= forcaPulo;
		tempoPulando++;
		if (tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			tempoPulando = 0;
		}
	}

	async Task Desenhar()
	{
		while (!estaMorto)
		{
			if (estaPulando)
				AplicaPulo();
			else
				AplicaGravidade();
			GerenciaCanos();
			if (VerificaColisao())
			{
				estaMorto = true;
				frameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(tempoEntreFrames);
		}
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		larguraJanela = w;
		alturaJanela = h;
	}

	void GerenciaCanos()
	{
		imgCanocima.TranslationX -= velocidade;
		imgCanobaixo.TranslationX -= velocidade;
		if (imgCanobaixo.TranslationX <= -larguraJanela)
		{
			imgCanobaixo.TranslationX = 0;
			imgCanocima.TranslationX = 0;
			var alturaMax = -100;
			var alturaMin = -imgCanobaixo.HeightRequest;
			imgCanocima.TranslationY = Random.Shared.Next((int)alturaMin, (int)alturaMax);
			imgCanobaixo.TranslationY = imgCanocima.TranslationY + alturaMin + aberturaMin + imgCanobaixo.HeightRequest;
			score++;
			if (score % 2 ==0)
			{
				velocidade++;
			}
			labelScore.Text = "Canos:" + score.ToString("D3");
		}

	}

	void OnGameOverClicked(object s, EventArgs e)
	{
		frameGameOver.IsVisible = false;
		Inicializar();
		Desenhar();
	}

	void Inicializar()
	{
		estaMorto = false;
		imgCanocima.TranslationX = -larguraJanela;
		imgCanobaixo.TranslationX = -larguraJanela;
		passaro.TranslationY = 0;
		score = 0;
		GerenciaCanos();
	}

	bool VerificaColisaoTeto()
	{
		var minY = -alturaJanela / 2;
		if (passaro.TranslationY <= minY)
			return true;
		else
			return false;
	}

	bool VerificaColisaoChao()
	{
		var maxY = alturaJanela / 2 - imgchao.HeightRequest;
		if (passaro.TranslationY >= maxY)
			return true;
		else
			return false;
	}
	bool VerificaColisao()
	{
	
	return VerificaColisaoTeto()||
	       VerificaColisaoChao()||
		   VerificaColisaoCanocima()||
		   VerificaColisaoCanobaixo();
	
	}
	 
		bool VerificaColisaoCanocima()
		{
		var posHpassaro = (larguraJanela / 2) - (passaro.WidthRequest / 2);
		var posVpassaro = (alturaJanela / 2) - (passaro.HeightRequest / 2) + passaro.TranslationY;
		if (posHpassaro >= Math.Abs(imgCanocima.TranslationX) - imgCanocima.WidthRequest &&
		    posHpassaro <= Math.Abs(imgCanocima.TranslationX) + imgCanocima.WidthRequest &&
		    posVpassaro <= imgCanocima.HeightRequest + imgCanocima.TranslationY)

		{
			return true;
		}
		else
		{
			return false;
		}
	}
	bool VerificaColisaoCanobaixo()
	{
		var posHpassaro = (larguraJanela/2) - (passaro.WidthRequest /2);
		var posVpassaro = (alturaJanela/2) + (passaro.HeightRequest /2) + passaro.TranslationY;
		var yMaxCano = imgCanocima.HeightRequest + imgCanocima.TranslationY + aberturaMin;
		if (posHpassaro >= Math.Abs(imgCanobaixo.TranslationX) - imgCanobaixo.WidthRequest &&
		    posHpassaro <= Math.Abs(imgCanobaixo.TranslationX) + imgCanobaixo.WidthRequest &&
		    posVpassaro >= yMaxCano)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	void PassaroSobe (object sender, EventArgs e)
	{
		estaPulando=true;
	}

	

}
