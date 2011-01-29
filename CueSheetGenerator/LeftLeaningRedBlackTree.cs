using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CueSheetGenerator {
    /// <summary>
    /// class left-leaning red-black tree, implements a
    /// self balancing binary search tree 
    /// </summary>
    public class LLRBTree {
        //indicates whether the node is red or black
        private const bool RED = true;
        private const bool BLACK = false;

        public int TreeSize {
            get { return _root.Size; }
        }

        //the root node in the tree
        private Node _root;

        //special nill node, allows us to not make 
        //node comparrisons to null and avoid conditionals
        //based on nodes equaling null.
        private Node _nill;
        private Node Nill {
            get { return _nill; }
            set { _nill = value; }
        }

        /// <summary>
        /// constructor for left leaning red black tree 
        /// </summary>
        public LLRBTree() {
            //initialize nill, use nill as the
            //children of all new nodes
            //its size is zero and its key is -1
            _nill = new Node(null, null, BLACK);
            _nill.Size = 0;
            _root = _nill;
        }

        /// <summary>
        /// class composition, the node composes part
        /// of the left leaning red black tree class
        /// </summary>
        public class Node {
            IComparable _key;
            internal IComparable Key {
                get { return _key; }
                set { _key = value; }
            }

            public object getKey() {
                return _key;
            }


            object _value;
            /// <summary>
            /// generic value member
            /// </summary>
            internal object Value {
                get { return _value; }
                set { _value = value; }
            }

            /// <summary>
            /// public get value function
            /// </summary>
            public object getValue() {
                return _value;
            }


            bool _color;
            /// <summary>
            /// red or black
            /// </summary>
            public bool Color {
                get { return _color; }
                set { _color = value; }
            }

            //left and right child nodes
            internal Node _left, _right;
            /// <summary>
            /// left child of node
            /// </summary>
            internal Node Left {
                get { return _left; }
                set { _left = value; }
            }
            /// <summary>
            /// right child of node
            /// </summary>
            internal Node Right {
                get { return _right; }
                set { _right = value; }
            }

            //the size of the tree
            private int _size;
            /// <summary>
            /// size of the subtree at node
            /// </summary>
            public int Size {
                get { return _size; }
                set { _size = value; }
            }

            /// <summary>
            /// constructor for node
            /// </summary>
            public Node(IComparable key, object value, bool color) {
                _key = key;
                _value = value;
                _color = color;
            }

            //default constructor
            public Node() { }
        }

        //left rotate
        private Node rotateLeft(Node p) {
            Node x = p.Right;
            p.Right = x.Left;
            x.Left = p;
            x.Color = x.Left.Color;
            x.Left.Color = RED;
            x.Size = p.Size;
            p.Size = p.Left.Size + p.Right.Size + 1;
            return x;
        }

        //right rotate
        private Node rotateRight(Node p) {
            Node x = p.Left;
            p.Left = x.Right;
            x.Right = p;
            x.Color = x.Right.Color;
            x.Right.Color = RED;
            x.Size = p.Size;
            p.Size = p.Left.Size + p.Right.Size + 1;
            return x;
        }

        //color flip
        private Node colorFlip(Node x) {
            x.Color = !x.Color;
            x.Left.Color = !x.Left.Color;
            x.Right.Color = !x.Right.Color;
            return x;
        }

        /// <summary>
        /// public insert function
        /// </summary>
        public void insert(IComparable key, object val) {
            //start insertion at the root node, pass by reference
            insert(ref _root, key, val);
            //root node can never be red
            if (isRed(_root)) _root.Color = BLACK;
        }

        private Node insert(ref Node x, IComparable key, object val) {
            if (x == _nill) {
                x = new Node(key, val, RED);
                x.Left = _nill;
                x.Right = _nill;
                x.Size = 1;
                return x;
            }
            x.Size++; //increment the size of each node on the way down
            int cmp = key.CompareTo(x.Key);
            if (cmp == 0) x.Value = val;
            else if (cmp < 0)
                x.Left = insert(ref x._left, key, val);
            else
                x.Right = insert(ref x._right, key, val);
            if (isRed(x.Right))
                x = rotateLeft(x);
            if (isRed(x.Left) && isRed(x.Left.Left))
                x = rotateRight(x);
            if (isRed(x.Left) && isRed(x.Right))
                colorFlip(x);
            return x;
        }

        //check if node is red, kind of superflous 
        private bool isRed(Node x) {
            if (x == _nill) return false;
            return (x.Color == RED);
        }


        /// <summary>
        /// get minima in the tree
        /// </summary>
        public IComparable min() {
            Node x = _root;
            while (x.Left != _nill) x = x.Left;
            return x.Key;
        }

        //private overload of min
        private IComparable min(Node x) {
            while (x.Left != _nill) x = x.Left;
            return x.Key;
        }

        private Node getMinNode() {
            Node x = _root;
            while (x.Left != _nill) x = x.Left;
            return x;
        }

        /// <summary>
        /// public find function
        /// </summary>
        public object find(IComparable key) {
            Node x = _root;
            while (x != _nill) {
                int cmp = key.CompareTo(x.Key);
                if (cmp == 0) return x.Value;
                else if (cmp < 0) x = x.Left;
                else if (cmp > 0) x = x.Right;
            }
            return null;
        }

        //private overload of find
        private object find(Node x, IComparable key) {
            while (x != _nill) {
                int cmp = key.CompareTo(x.Key);
                if (cmp == 0) return x.Value;
                else if (cmp < 0) x = x.Left;
                else if (cmp > 0) x = x.Right;
            }
            return null;
        }

        //move red left, used to maintian properties
        //of the left leaning red black tree
        private Node moveRedLeft(Node x) {
            colorFlip(x);
            if (isRed(x.Right.Left)) {
                x.Right = rotateRight(x.Right);
                x = rotateLeft(x);
                colorFlip(x);
            }
            return x;
        }

        /// <summary>
        /// delete minima, dont use this, it does not work at present
        /// </summary>
        public void deleteMin() {
            _root = deleteMin(getMinNode());
            _root.Color = BLACK;
        }

        //private overload of delete min
        private Node deleteMin(Node x) {
            if (x.Left == _nill)
                return null;
            if (!isRed(x.Left) && !isRed(x.Left.Left))
                x = moveRedLeft(x);
            x.Left = deleteMin(x.Left);
            return fixUp(x);
        }

        //private delete function 
        private Node delete(Node x, IComparable key) {
            int cmp = key.CompareTo(x.Key);
            if (cmp < 0) {
                if (!isRed(x.Left) && !isRed(x.Left.Left))
                    x = moveRedLeft(x);
                x.Left = delete(x.Left, key);
            } else {
                //wtf is leanRight, it is undefined in the slides
                //maybe it should be something else like moveRedRight
                //maybe rotate right
                //if (isRed(x.Left)) x = leanRight(x);
                if (cmp == 0 && (x.Right == _nill))
                    return null;
                if (!isRed(x.Right) && !isRed(x.Right.Left))
                    x = moveRedRight(x);
                if (cmp == 0) {
                    x.Key = min(x.Right);
                    x.Value = find(x.Right, x.Key);
                    x.Right = deleteMin(x.Right);
                } else x.Right = delete(x.Right, key);
            }
            return fixUp(x);
        }

        //fix up
        private Node fixUp(Node x) {
            if (isRed(x.Right))
                x = rotateLeft(x);
            if (isRed(x.Left) && isRed(x.Left.Left))
                x = rotateRight(x);
            if (isRed(x.Left) && isRed(x.Right))
                colorFlip(x);
            return x;
        }

        //move right
        private Node moveRedRight(Node x) {
            colorFlip(x);
            if (isRed(x.Left.Left)) {
                x = rotateRight(x);
                colorFlip(x);
            }
            return x;
        }

        /// <summary>
        /// order statistics get rank function
        /// </summary>
        public IComparable getRank(IComparable key) {
            return osRank(key);
        }

        //find a node with a given rank
        private Node osSelect(Node x, int i) {
            int r = x.Left.Size + 1;
            if (i == r) return x;
            else if (i < r)
                return osSelect(x.Left, i);
            else
                return osSelect(x.Right, i - r);
        }

        //determine the rank of an element
        //top down approach, modified find function
        private IComparable osRank(IComparable key) {
            Node x = _root; //start at the root node
            int cmp = 0, r = x.Right.Size + 1;
            while (x != _nill) {            //compare to "nill" node
                cmp = key.CompareTo(x.Key); //compare keys
                if (cmp == 0) return r;     //if equal return rank
                else if (cmp < 0) {
                    x = x.Left;             //go left
                    r = r + 1 + x.Right.Size;
                } else {
                    x = x.Right;            //go right  
                    r = r - 1 - x.Left.Size;
                }
            }
            return null; //did not find the key in the tree
        }

        private List<Node> getPreOrederList(Node x) {
            if (x != _nill) {
                _nodeValuesInTree.Add(x);
                getPreOrederList(x.Left);
                getPreOrederList(x.Right);
            }
            return _nodeValuesInTree;
        }

        List<Node> _nodeValuesInTree = null;
        /// <summary>
        /// gets a pre-ordered list of the elements in the tree
        /// </summary>
        public List<Node> getPreOrederList() {
            _nodeValuesInTree = new List<Node>();
            return getPreOrederList(_root);
        }

    }

}
