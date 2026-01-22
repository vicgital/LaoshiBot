using System;
using System.Collections.Generic;
using System.Text;

namespace LaoshiBot.Application.Interfaces.Components
{
    public interface IDalleImageGeneratorComponent
    {
        Task<Stream?> GenerateImageFromChineseTextAsync(string text);
    }

}
