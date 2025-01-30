using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Models.APIModels;
public sealed class AuthResponse
{
	public bool Success { get; set; }
	public string[]? Errors { get; set; }
}
