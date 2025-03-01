using MyEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement
{
    public abstract class DisplayMenu
    {
        public static void TopMenu(Heading head1, Heading head2)
        {
            short value1 = (short)head1;
            var heading1 = EnumsCl.GetEnumDisplay((Heading)value1);
            short value2 = (short)head2;
            var heading2 = EnumsCl.GetEnumDisplay((Heading)value2);

            int rectangleWidth = 50;
            int rectangleHeight = 5;

            Console.SetCursorPosition(5, 2);
            Console.WriteLine("┌" + new string('─', rectangleWidth) + "┐"); // Draw top border of rectangle

            for (int i = 0; i < rectangleHeight; i++)
            {
                Console.SetCursorPosition(5, 3 + i);
                Console.WriteLine("│" + new string(' ', rectangleWidth) + "│"); // Draw vertical borders of rectangle
            }

            Console.SetCursorPosition(5, 3 + rectangleHeight);
            Console.WriteLine("└" + new string('─', rectangleWidth) + "┘"); // Draw bottom border of rectangle

            // Add headings inside the rectangle
            Console.SetCursorPosition(7, 4);
            Console.WriteLine(heading1);

            Console.SetCursorPosition(7, 6);
            Console.WriteLine(new string('-', rectangleWidth - 4)); // Dotted line

            Console.SetCursorPosition(7, 7);
            Console.WriteLine(heading2);

            Console.WriteLine("\n\n\n");

        }
        public abstract void MenuList();

    }

    public static class EnumsCl
    {
        public static string GetEnumDisplay(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DisplayAttribute[] attributes = (DisplayAttribute[])fi.GetCustomAttributes(typeof(DisplayAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Name;
            else
                return value.ToString();
        }
    }
}
