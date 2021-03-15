using FluentValidation;
using WebApplication1.ViewModel;

namespace WebApplication1.Validators
{
    public class ArticuloValidator : AbstractValidator<ArticuloVM>
    {
        public ArticuloValidator()
        {
            RuleFor(x => x.Codigo)
                .NotNull().Length(5, 5)
                .WithMessage("Ingresar código de 5 dígitos");
            RuleFor(x => x.Descripcion)
                .NotNull().WithMessage("Campo Requerido");
            RuleFor(x => x.Precio)
                .NotNull().WithMessage("Campo requerido")
                .ScalePrecision(4, 9)
                .Must(x => (x >= 0) && x <= 10000);
            RuleFor(x => x.Costo)
                .NotNull().WithMessage("Campo requerido")
                .ScalePrecision(4, 9)
                .Must(x => (x >= 0) && x <= 10000);
        }
    }
}
