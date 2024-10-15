﻿using System.Threading.Channels;

namespace flappy_bird;

public partial class MainPage : ContentPage
{
	
const int gravidade = 8;
const int tempoEntreFrames = 25;
bool estaMorto = true;
double larguraJanela=0;
double alturaJanela=0;
int velocidade=10;
const int maxTempoPulando = 2;
int tempoPulando = 0;
bool estaPulando = false;
const int forcaPulo = 40;
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
	
	async Task Desenhar ()
	{
      while (! estaMorto) 
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
 
 bool VerificaColisaoTeto()
 {
	var minY = -alturaJanela/2;
	if (passaro.TranslationY <= minY)
	return true;
	else
	return false;
 }

 bool VerificaColisaoChao()
 {
	var maxY = alturaJanela/2 - imgchao.HeightRequest;
	if (passaro.TranslationY >= maxY)
	return true;
	else
	return false;
 }
 bool VerificaColisao()
 {
	if (!estaMorto)
	{
   if (VerificaColisaoTeto()|| 
   VerificaColisaoChao())
   {
	return true;
   }
	}
	return false;
 }
 void PatyChataDesmais(object s,TappedEventArgs a)
 {
	estaPulando=true;
 }

 
 



}

