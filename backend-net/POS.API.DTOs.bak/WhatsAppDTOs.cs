namespace POS.API.DTOs;

public class WhatsAppStatusDto
{
    public string Status { get; set; } = "uninitialized";
    public bool Ready { get; set; }
    public bool HasQr { get; set; }
    public string? Error { get; set; }
}

public class WhatsAppQrDto
{
    public string? Qr { get; set; }
    public string? Code { get; set; }
}

public class WhatsAppReconnectDto
{
    public string Message { get; set; } = string.Empty;
}
