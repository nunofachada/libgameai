/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 *
 * Author: Nuno Fachada
 * */

using System;
using System.Collections.Generic;

namespace BehaviorTree
{
    // Provides additional methods for classes which implement the IList<T>
    // interface
    public static class IListExtensions
    {
        // Randomly shuffle the contents of the list
        public static void Shuffle<T>(
            this IList<T> list, Func<int, int> nextInt)
        {
            int n = list.Count;
            while (n > 1) {
                T aux;
                int k = nextInt(n);
                n--;
                aux = list[n];
                list[n] = list[k];
                list[k] = aux;
            }
        }
    }
}