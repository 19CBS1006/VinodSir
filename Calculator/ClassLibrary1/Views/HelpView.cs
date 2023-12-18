using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorComponent.Views
{
    public class HelpView
    {
        public void CreateHelpView()
        {
            Console.WriteLine(CalculatorCommonOutput.CreateHelpHeading());
            Console.WriteLine("# Press [=] to hide");

            Console.WriteLine($@"{Environment.NewLine}

                ctrl+l->memoy view
                ctr+k->memory Recall    MR
                ctrl+B->memory add    M+
                ctrl+U->memory minus  M-
                ctr+S->memory save    MS



                UpArrow->toggle between degree/rad
                shift+s->to perform Sin  (based on toggle rad/deg for now)
                shift+E->scientific notation (e)
                shift+y->log
                shift+G->Power
                shift+A->absolute
                ctrl+Q->ceiling
                shift+f->floor
                shift+G->Power Operation (basically x^y)
                shift+c->cos

                oemPlus->show result
                bakcspace->clear prev character
                Delete-> clear all data in calclator console"

                );

        }
    }
}