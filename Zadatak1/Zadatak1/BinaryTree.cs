using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    public class BinaryTree<T>
    {
        private Node _root;
       

        public Node Root
        {
            get { return _root; }
            set { _root = value; }
        }

    }
}
