﻿using Monxterz.StatePlatform.Client;

namespace Monxterz.SquadRpg.MauiClient;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
