using shop.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.Email
{
	public interface IEmailSender
	{
		void Send(SendEmailDto dto);
	}
}
