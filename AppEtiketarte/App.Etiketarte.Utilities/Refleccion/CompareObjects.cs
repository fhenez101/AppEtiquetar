using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace App.Etiketarte.Utilities.Refleccion
{
    public class CompareObjects<TModel> where TModel : class
    {
        public ObservableCollection<string> Compare(TModel objectA, TModel objectB, params string[] ignoreList)
        {
            var result = new ObservableCollection<string>();

            if (objectA != null && objectB != null)
            {
                objectA.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && !ignoreList.Contains(p.Name))
                    .ToList()
                    .ForEach(x =>
                    {
                        object valueA = x.GetValue(objectA, null);
                        object valueB = x.GetValue(objectB, null);

                        if (CanDirectlyCompare(x.PropertyType))
                        {
                            if (!AreValuesEqual(valueA, valueB))
                            {
                                result.Add(x.Name);
                            }
                        }
                    });
            }

            return result;
        }

        private static bool CanDirectlyCompare(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
        }

        private bool AreValuesEqual(object valueA, object valueB)
        {
            var selfValueComparer = valueA as IComparable;

            // one of the values is null
            if (valueA == null || valueB == null)
            {
                return false;
            }
            // the comparison using IComparable failed
            else if (selfValueComparer != null && selfValueComparer.CompareTo(valueB) != 0)
            {
                return false;
            }
            // the comparison using Equals failed
            else if (!Equals(valueA, valueB))
            {
                return false;
            }
            // match
            else
            {
                return true;
            }
        }
    }
}