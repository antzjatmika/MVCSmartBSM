using ApiHelper.Models;
namespace ApiHelper
{
    public interface ITokenContainer
    {
        //token untuk header validasi
        object ApiToken { get; set; }
        //role apa saja yang diassign ke user
        object TokenRole { get; set; }
        //role pertama yang dimiliki user (saat ini masih berlaku satu role untuk tiap user)
        object RoleName { get; set; }
        //id rekanan /id kar/id pcp/id portfolio/id adminsistem
        object UserId { get; set; }
        object SupervisorId { get; set; }
        object IdRekananContact { get; set; }
        object IdNotaris { get; set; }
        object IdOrganisasi { get; set; }
        object UserName { get; set; }
        object UserEmail { get; set; }
        object IdTypeOfRekanan { get; set; }
        //object XLSPointer { get; set; }
        object Keterangan { get; set; }
    }
}