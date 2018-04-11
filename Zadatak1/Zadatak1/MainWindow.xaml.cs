using Microsoft.Expression.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Zadatak1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ArrayList path = new ArrayList();
        ArrayList firstPath = new ArrayList();
        ArrayList secondPath = new ArrayList();
        BinaryTree<int> btree = new BinaryTree<int>();
        Dictionary<Node, TextBox> nodes = new Dictionary<Node, TextBox>();
        List<TextBox> tb_s = new List<TextBox>();
        List<TextBox> tbSPath = new List<TextBox>();
        List<LineArrow> connectionLines = new List<LineArrow>();
        List<TextBox> selected = new List<TextBox>(2);
        int cnt_selected;
        int node_cnt = 0;
        bool validate = false;
        public MainWindow()
        {
            InitializeComponent();

            #region FillTBoxList
            tb_s.Add(tb_root);
            tb_s.Add(tb_root_left);
            tb_s.Add(tb_root_right);
            tb_s.Add(tb_root_left_left);
            tb_s.Add(tb_root_left_right);
            tb_s.Add(tb_root_left_left_left);
            tb_s.Add(tb_root_left_left_right);
            tb_s.Add(tb_root_left_right_left);
            tb_s.Add(tb_root_left_right_right);
            tb_s.Add(tb_root_right_left);
            tb_s.Add(tb_root_right_right);
            tb_s.Add(tb_root_right_right_left);
            tb_s.Add(tb_root_right_right_right);
            tb_s.Add(tb_root_right_left_right);
            tb_s.Add(tb_root_right_left_left);

            #endregion

            #region AddInitialNodes
            Random r = new Random();
            btree.Root = new Node(r.Next());
            btree.Root.Left = new Node(r.Next());
            btree.Root.Left.Left = new Node(r.Next());
            btree.Root.Left.Right = new Node(r.Next());
            btree.Root.Right = new Node(r.Next());
            btree.Root.Left.Left.Left = new Node(r.Next());
            btree.Root.Left.Left.Right = new Node(r.Next());
            btree.Root.Left.Right.Left = new Node(r.Next());
            btree.Root.Left.Right.Right = new Node(r.Next());
            btree.Root.Right.Left = new Node(r.Next());
            btree.Root.Right.Right = new Node(r.Next());
            btree.Root.Right.Right.Left = new Node(r.Next());
            btree.Root.Right.Right.Right = new Node(r.Next());
            btree.Root.Right.Left.Right = new Node(r.Next());
            btree.Root.Right.Left.Left = new Node(r.Next());
            #endregion

            #region FillDictionaryNodes
            nodes.Add(btree.Root, tb_root);
            nodes.Add(btree.Root.Left, tb_root_left);
            nodes.Add(btree.Root.Right, tb_root_right);
            nodes.Add(btree.Root.Left.Left, tb_root_left_left);
            nodes.Add(btree.Root.Left.Right, tb_root_left_right);
            nodes.Add(btree.Root.Left.Left.Left, tb_root_left_left_left);
            nodes.Add(btree.Root.Left.Left.Right, tb_root_left_left_right);
            nodes.Add(btree.Root.Left.Right.Left, tb_root_left_right_left);
            nodes.Add(btree.Root.Left.Right.Right, tb_root_left_right_right);
            nodes.Add(btree.Root.Right.Left, tb_root_right_left);
            nodes.Add(btree.Root.Right.Right, tb_root_right_right);
            nodes.Add(btree.Root.Right.Right.Left, tb_root_right_right_left);
            nodes.Add(btree.Root.Right.Right.Right, tb_root_right_right_right);
            nodes.Add(btree.Root.Right.Left.Right, tb_root_right_left_right);
            nodes.Add(btree.Root.Right.Left.Left, tb_root_right_left_left);
            #endregion

            #region SetLinesToCanvas
            connectionLines.Add(ln_root_left);
            connectionLines.Add(ln_root_left_left);
            connectionLines.Add(ln_root_left_left_left);
            connectionLines.Add(ln_root_left_left_right);
            connectionLines.Add(ln_root_left_right);
            connectionLines.Add(ln_root_left_right_left);
            connectionLines.Add(ln_root_left_right_right);
            connectionLines.Add(ln_root_right_left);
            connectionLines.Add(ln_root_rigth_right_left);
            connectionLines.Add(ln_root_rigth_right_right);
            connectionLines.Add(ln_root_right);
            connectionLines.Add(ln_root_rigth_right);
            connectionLines.Add(ln_root_rigth_left_right);
            connectionLines.Add(ln_root_rigth_left_left);
            #endregion

            #region SetTextBoxBehavioral
            foreach (TextBox tb in tb_s)
            {
                tb.Visibility = Visibility.Hidden;
                tb.IsReadOnly = true;
                tb.TextAlignment = TextAlignment.Center;
                tb.FontSize = 12;
            }
            #endregion

            #region SetLineArrowsBehavioral
            foreach (LineArrow la in connectionLines)
            {
                la.Visibility = Visibility.Hidden;
            }
            #endregion
        }


        private void btn_AddNode_Click(object sender, RoutedEventArgs e)
        {        
            if (tb.Text != "")
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, "[^0-9]"))
                {
                    tb.ToolTip = "Only number is allowed";
                }
                else
                {
                    foreach(TextBox t in tb_s)
                    {
                        if(t.Text == tb.Text)
                        {
                            tb.ToolTip = "Not allowed duplicate values.";
                            validate = false;
                            break;
                        }
                        else
                        {
                            tb.ToolTip = null;
                            validate = true;
                        }
                    }
                  
                }
            }

            if(validate == true)
            {
                node_cnt++;
                FillVisualTree(int.Parse(tb.Text), node_cnt);
            }
           
        }
        private void ResetVisualPath()
        {
            foreach (TextBox tbs in tb_s)
            {
                tbs.Background = Brushes.LightGray;
            }
        }
        private void FillVisualTree(int v, int cnt)
        {
            if (cnt == 1)
            {
                tb_root.Visibility = Visibility.Visible;
                tb_root.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 2)
            {
                tb_root_left.Visibility = Visibility.Visible;
                ln_root_left.Visibility = Visibility.Visible;
                tb_root_left.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 3)
            {
                tb_root_right.Visibility = Visibility.Visible;
                ln_root_right.Visibility = Visibility.Visible;
                tb_root_right.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 4)
            {
                tb_root_left_left.Visibility = Visibility.Visible;
                ln_root_left_left.Visibility = Visibility.Visible;
                tb_root_left_left.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 5)
            {
                tb_root_left_right.Visibility = Visibility.Visible;
                ln_root_left_right.Visibility = Visibility.Visible;
                tb_root_left_right.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 6)
            {
                tb_root_right_left.Visibility = Visibility.Visible;
                ln_root_right_left.Visibility = Visibility.Visible;
                tb_root_right_left.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 7)
            {
                tb_root_right_right.Visibility = Visibility.Visible;
                ln_root_rigth_right.Visibility = Visibility.Visible;
                tb_root_right_right.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 8)
            {
                tb_root_left_left_left.Visibility = Visibility.Visible;
                ln_root_left_left_left.Visibility = Visibility.Visible;
                tb_root_left_left_left.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 9)
            {
                tb_root_left_left_right.Visibility = Visibility.Visible;
                ln_root_left_left_right.Visibility = Visibility.Visible;
                tb_root_left_left_right.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 10)
            {
                tb_root_left_right_left.Visibility = Visibility.Visible;
                ln_root_left_right_left.Visibility = Visibility.Visible;
                tb_root_left_right_left.Text = v.ToString();
                FillTree(v, cnt);

            }
            else if (cnt == 11)
            {
                tb_root_left_right_right.Visibility = Visibility.Visible;
                ln_root_left_right_right.Visibility = Visibility.Visible;
                tb_root_left_right_right.Text = v.ToString();
                FillTree(v, cnt);

            }
            else if (cnt == 12)
            {
                tb_root_right_left_left.Visibility = Visibility.Visible;
                ln_root_rigth_left_left.Visibility = Visibility.Visible;
                tb_root_right_left_left.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 13)
            {
                tb_root_right_left_right.Visibility = Visibility.Visible;
                ln_root_rigth_left_right.Visibility = Visibility.Visible;
                tb_root_right_left_right.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 14)
            {
                tb_root_right_right_left.Visibility = Visibility.Visible;
                ln_root_rigth_right_left.Visibility = Visibility.Visible;
                tb_root_right_right_left.Text = v.ToString();
                FillTree(v, cnt);
            }
            else if (cnt == 15)
            {
                tb_root_right_right_right.Visibility = Visibility.Visible;
                ln_root_rigth_right_right.Visibility = Visibility.Visible;
                tb_root_right_right_right.Text = v.ToString();
                FillTree(v, cnt);

            }

        }
        private void FillTree(int v, int cnt_node)
        {
            if (cnt_node == 1)
            {
                btree.Root = new Node(v);
            }
            else if (cnt_node == 2)
            {
                btree.Root.Left = new Node(v);
            }
            else if (cnt_node == 3)
            {
                btree.Root.Right = new Node(v);
            }
            else if (cnt_node == 4)
            {
                btree.Root.Left.Left = new Node(v);
            }
            else if (cnt_node == 5)
            {
                btree.Root.Left.Right = new Node(v);
            }
            else if (cnt_node == 6)
            {
                btree.Root.Right.Left = new Node(v);
            }
            else if (cnt_node == 7)
            {
                btree.Root.Right.Right = new Node(v);
            }
            else if (cnt_node == 8)
            {
                btree.Root.Left.Left.Left = new Node(v);
            }
            else if (cnt_node == 9)
            {
                btree.Root.Left.Left.Right = new Node(v);
            }
            else if (cnt_node == 10)
            {
                btree.Root.Left.Right.Left = new Node(v);
            }
            else if (cnt_node == 11)
            {
                btree.Root.Left.Right.Right = new Node(v);
            }
            else if (cnt_node == 12)
            {
                btree.Root.Right.Left.Left = new Node(v);
            }
            else if (cnt_node == 13)
            {
                btree.Root.Right.Left.Right = new Node(v);
            }
            else if (cnt_node == 14)
            {
                btree.Root.Right.Right.Left = new Node(v);
            }
            else if (cnt_node == 15)
            {
                btree.Root.Right.Right.Right = new Node(v);
            }
        }
        public bool PrintPathForSelectedNode(Node root, Node dest)
        {
            if (root == null)
                return false;
            if (root == dest || PrintPathForSelectedNode(root.Right, dest) || PrintPathForSelectedNode(root.Left, dest))
            {
                path.Add(root.Value);
                return true;
            }
            return false;
        }
        private void LowestCommonAchestor(List<TextBox> selected)
        {
            /*When find selected nodes, send both to function which find path to root
             from selected nodes and find same value in paths--> this is LowestCommonEdge*/
            #region FindSelectedNodes
            foreach (var node in nodes)
            {
                if (node.Value.Name == selected[selected.Count - 2].Name)
                {
                    if (selected[selected.Count - 2].Name == "tb_root")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_right_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Right);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_left_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Left);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_left_left_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Left.Left);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_right_right_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Right.Right);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_right_right_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Right.Right);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_right_left_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Left.Right);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_right_left_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Left.Left);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_right_right_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Right.Left);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_right_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Left);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_left_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Right);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_left_right_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Right.Left);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_left_right_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Right.Right);
                        firstPath = path;
                    }
                    else if (selected[selected.Count - 2].Name == "tb_root_left_left_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Left.Right);
                        firstPath = path;
                    }

                }
                else if (node.Value.Name == selected[selected.Count - 1].Name)
                {
                    if (selected[selected.Count - 1].Name == "tb_root")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_right_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Right);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_left_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Left);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_left_left_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Left.Left);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_right_right_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Right.Right);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_right_right_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Right.Right);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_right_left_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Left.Right);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_right_left_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Left.Left);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_right_right_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Right.Left);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_right_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Right.Left);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_left_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Right);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_left_right_left")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Right.Left);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_left_right_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Right.Right);
                        secondPath = path;
                    }
                    else if (selected[selected.Count - 1].Name == "tb_root_left_left_right")
                    {
                        path = new ArrayList();
                        PrintPathForSelectedNode(btree.Root, btree.Root.Left.Left.Right);
                        secondPath = path;
                    }
                }

            }
            #endregion

            #region Print LCE to label1
            path = new ArrayList();
            foreach (int y in firstPath)
            {
                if (secondPath.Contains(y))
                {
                    foreach (TextBox lce_tb in tb_s)
                    {
                        if (lce_tb.Text == y.ToString())
                        {
                            label1.Content = y.ToString();
                        }
                    }
                    break;
                }
            }
            #endregion
        }

        #region EventsOnClickNode
        private void tb_root_left_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }

            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Left);
            path.Reverse();

            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root);

            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_right_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Right);
            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_left_left_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Left.Left);
            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_left_right_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);

            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }

            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Left.Right);
            path.Reverse();

            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_right_left_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Right.Left);
            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_right_right_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Right.Right);
            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_left_left_left_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Left.Left.Left);
            path.Reverse();



            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_left_left_right_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Left.Left.Right);
            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_left_right_left_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Left.Right.Left);
            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_left_right_right_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Left.Right.Right);
            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_right_right_left_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Right.Right.Left);
            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_right_right_right_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Right.Right.Right);
            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }

                }

            }
        }

        private void tb_root_right_left_right_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Right.Left.Right);
            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }
        }

        private void tb_root_right_left_left_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            selected.Add((TextBox)e.Source);
            if (cnt_selected++ > 1)
            {
                LowestCommonAchestor(selected);
            }
            ResetVisualPath();
            path = new ArrayList();
            PrintPathForSelectedNode(btree.Root, btree.Root.Right.Left.Left);
            path.Reverse();


            for (int j = 0; j < path.Count; j++)
            {
                foreach (TextBox tbPath in tb_s)
                {
                    if (tbPath.Text == path[j].ToString())
                    {
                        tbPath.Background = Brushes.LightBlue;
                    }
                }

            }

        }
        #endregion

    }
}
