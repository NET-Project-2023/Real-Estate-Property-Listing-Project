
namespace Real_estate.Domain.Common
{
    //entitate auditabila- vrei sa stii care-i userul si cine a facut modificari pe anumita entitate in orice moment
    //Scopul clasei e sa o atasam de alta clase de domeniu a.i. sa stim cand si cine a modifcat acea clasa dupa autentificare,
    //practic sistemu va sti cine a facut ultimu schimbare :)
    public class AuditableEntity// pe astea nu le livram noi, se constr un
                                // mecanis intern cu entity framework care
                                // va citi userul curent dintr un context
                                // http si va construi singur astea
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

    }
}
