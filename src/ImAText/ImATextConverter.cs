using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Linq;
using ImAText.Exceptions;
using System.Collections.ObjectModel;

namespace ImAText
{
    public class ImATextConverter
    {
        private Bitmap _imageFile;

        private StringBuilder _sb = new StringBuilder();

        private Dictionary<int, string> _redValues = new Dictionary<int, string>()
        {
            { 0, "@" },
            { 50, "#" },
            { 70, "8" },
            { 100, "&" },
            { 130, "o" },
            { 160, ":" },
            { 180, "*" },
            { 200, " " }
        };

        public ReadOnlyDictionary<int, string> RedValues { get; init; }

        public ImATextConverter(Bitmap imageFile)
        {
            if (imageFile.IsGrayScale())
                _imageFile = imageFile.Resize(100, 100);
            else
                _imageFile  = imageFile.ConvertToGrayScale();

            RedValues = new ReadOnlyDictionary<int, string>(_redValues);
        }

        public void AddRedValue(int redValue, string character)
        {
            if (RedValues.Keys.Any(key => key == redValue))
                throw new KeyAlreadyExistsException(redValue);

            if (RedValues.Values.Any(value => value == character))
                throw new ValueAlreadyExistsException(character);

            _redValues.Add(redValue, character);

            _redValues.OrderBy(kv => kv.Key);
        }

        public void RemoveRedValue(int redValue)
        {
            throw new NotImplementedException();
        }

        public string GetTextifiedImage()
        {
            _sb.Clear();

            for (var i = 0; i < _imageFile.Height; i++)
            {
                for (var j = 0; j < _imageFile.Width; j++)
                {
                    var pixel = _imageFile.GetPixel(j, i);

                    var desiredKey = BinarySearch(pixel.R);

                    _sb.Append(_redValues[desiredKey]);

                    if (j == _imageFile.Width - 1)
                        _sb.Append("\n");   
                }
            }

            return _sb.ToString();
        }


        private int BinarySearch(byte redValue)
        {
            var keysList = _redValues.Keys.ToArray();

            if (redValue < keysList[0])
                return keysList[0];

            if (redValue > keysList[keysList.Length - 1])
                return keysList[keysList.Length - 1];

            int mid;
            var low = 0;
            var high = keysList.Length - 1;

            while (low <= high)
            {
                mid = (high + low) / 2;

                if (redValue < keysList[mid])
                    high = mid - 1;
                else if (redValue > keysList[mid])
                    low = mid + 1;
                else
                    return keysList[mid];
            }

            return (keysList[low] - redValue) < (redValue - keysList[high]) 
                ? keysList[low] 
                : keysList[high];
        }
    }

}
