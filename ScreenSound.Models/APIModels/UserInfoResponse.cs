﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Models.APIModels;
public sealed class UserInfoResponse
{
	public string? Email { get; set; }
	public bool IsEmailVerified { get; set; }
}
