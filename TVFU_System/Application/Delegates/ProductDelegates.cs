﻿using DomainLayer.EventArgs;

namespace Application.Delegates
{
    public delegate ProductEventArgs ProductEventHandler(object sender, ProductEventArgs args);
}