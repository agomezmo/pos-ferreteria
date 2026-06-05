using System;
using System.Threading.Tasks;

namespace POS.API.Services;

public class WhatsAppService
{
    private string _status = "uninitialized";
    private bool _ready = false;
    private bool _hasQr = false;
    private string? _error = null;

    public WhatsAppService()
    {
    }

    public object GetStatus()
    {
        return new
        {
            status = _status,
            ready = _ready,
            hasQr = _hasQr,
            error = _error,
        };
    }

    public string? GetQr()
    {
        if (!_hasQr) return null;
        return "qr_placeholder";
    }

    public async Task ReconnectAsync()
    {
        _status = "initializing";
        _ready = false;
        _hasQr = false;
        _error = null;

        await Task.Delay(1000);

        _status = "qr_ready";
        _hasQr = true;
    }

    public async Task<bool> SendMessageAsync(string phone, string message)
    {
        if (!_ready)
        {
            _error = "WhatsApp no conectado";
            return false;
        }

        await Task.CompletedTask;
        return true;
    }
}
