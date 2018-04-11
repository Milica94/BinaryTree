using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    public class Node
    {
        private int _value;
        private Node _left;
        private Node _right;
        private int _children;

        public int Children
        {
            get { return _children; }
            set { _children = value; }
        }


        public Node Right
        {
            get { return _right; }
            set { _right = value; }
        }

        public Node Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public Node()
        {

        }
        public Node(int value)
        {
            this._value = value;
            this._left = null;
            this._right = null;
        }


    }
}
