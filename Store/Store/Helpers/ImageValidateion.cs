using System.ComponentModel.DataAnnotations;

namespace Store.Helpers
{
    public class ImageValidateion : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var imageURL = value as string;

            if (imageURL == null)
            {
                return true;
            }

            return imageURL.EndsWith(".jpg") ||
                imageURL.EndsWith(".png")||
                imageURL.EndsWith(".jpeg")||
                imageURL.EndsWith(".gif");

          
        }
    }
}