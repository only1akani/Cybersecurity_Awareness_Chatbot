using System.Collections;

namespace demo
{
    public class respond
    {
        public respond( ArrayList reply  , ArrayList ignore  )
        {//start of constructor

            answers(reply);
            words(ignore);


        }//end of constructor

        //method to store the list of words
        private void words(ArrayList ignoring)
        {//
         //ignoring questions
            ignoring.Add("a");
            ignoring.Add("about");
            ignoring.Add("above");
            ignoring.Add("across");
            ignoring.Add("after");
            ignoring.Add("afterwards");
            ignoring.Add("again");
            ignoring.Add("against");
            ignoring.Add("all");
            ignoring.Add("almost");
            ignoring.Add("alone");
            ignoring.Add("along");
            ignoring.Add("already");
            ignoring.Add("also");
            ignoring.Add("although");
            ignoring.Add("always");
            ignoring.Add("am");
            ignoring.Add("among");
            ignoring.Add("amongst");
            ignoring.Add("amount");
            ignoring.Add("an");
            ignoring.Add("and");
            ignoring.Add("another");
            ignoring.Add("any");
            ignoring.Add("anyhow");
            ignoring.Add("anyone");
            ignoring.Add("anything");
            ignoring.Add("anyway");
            ignoring.Add("anywhere");
            ignoring.Add("are");
            ignoring.Add("around");
            ignoring.Add("as");
            ignoring.Add("at");
            ignoring.Add("back");
            ignoring.Add("be");
            ignoring.Add("became");
            ignoring.Add("because");
            ignoring.Add("become");
            ignoring.Add("becomes");
            ignoring.Add("becoming");
            ignoring.Add("been");
            ignoring.Add("before");
            ignoring.Add("beforehand");
            ignoring.Add("behind");
            ignoring.Add("being");
            ignoring.Add("below");
            ignoring.Add("beside");
            ignoring.Add("besides");
            ignoring.Add("between");
            ignoring.Add("beyond");
            ignoring.Add("both");
            ignoring.Add("but");
            ignoring.Add("by");
            ignoring.Add("can");
            ignoring.Add("cannot");
            ignoring.Add("could");
            ignoring.Add("did");
            ignoring.Add("do");
            ignoring.Add("does");
            ignoring.Add("doing");
            ignoring.Add("done");
            ignoring.Add("down");
            ignoring.Add("during");
            ignoring.Add("each");
            ignoring.Add("either");
            ignoring.Add("else");
            ignoring.Add("elsewhere");
            ignoring.Add("enough");
            ignoring.Add("etc");
            ignoring.Add("even");
            ignoring.Add("ever");
            ignoring.Add("every");
            ignoring.Add("everyone");
            ignoring.Add("everything");
            ignoring.Add("everywhere");
            ignoring.Add("except");
            ignoring.Add("few");
            ignoring.Add("first");
            ignoring.Add("for");
            ignoring.Add("former");
            ignoring.Add("formerly");
            ignoring.Add("from");
            ignoring.Add("further");
            ignoring.Add("had");
            ignoring.Add("has");
            ignoring.Add("have");
            ignoring.Add("having");
            ignoring.Add("he");
            ignoring.Add("hence");
            ignoring.Add("her");
            ignoring.Add("here");
            ignoring.Add("hereafter");
            ignoring.Add("hereby");
            ignoring.Add("herein");
            ignoring.Add("hereupon");
            ignoring.Add("hers");
            ignoring.Add("herself");
            ignoring.Add("him");
            ignoring.Add("himself");
            ignoring.Add("his");
            ignoring.Add("how");
            ignoring.Add("however");
            ignoring.Add("i");
            ignoring.Add("if");
            ignoring.Add("in");
            ignoring.Add("indeed");
            ignoring.Add("inside");
            ignoring.Add("instead");
            ignoring.Add("into");
            ignoring.Add("is");
            ignoring.Add("it");
            ignoring.Add("its");
            ignoring.Add("itself");
            ignoring.Add("last");
            ignoring.Add("later");
            ignoring.Add("latter");
            ignoring.Add("latterly");
            ignoring.Add("least");
            ignoring.Add("less");
            ignoring.Add("lot");
            ignoring.Add("many");
            ignoring.Add("may");
            ignoring.Add("me");
            ignoring.Add("meanwhile");
            ignoring.Add("might");
            ignoring.Add("more");
            ignoring.Add("moreover");
            ignoring.Add("most");
            ignoring.Add("mostly");
            ignoring.Add("much");
            ignoring.Add("must");
            ignoring.Add("my");
            ignoring.Add("myself");
            ignoring.Add("name");
            ignoring.Add("namely");
            ignoring.Add("neither");
            ignoring.Add("never");
            ignoring.Add("nevertheless");
            ignoring.Add("next");
            ignoring.Add("no");
            ignoring.Add("nobody");
            ignoring.Add("none");
            ignoring.Add("noone");
            ignoring.Add("nor");
            ignoring.Add("not");
            ignoring.Add("nothing");
            ignoring.Add("now");
            ignoring.Add("nowhere");
            ignoring.Add("of");
            ignoring.Add("off");
            ignoring.Add("often");
            ignoring.Add("on");
            ignoring.Add("once");
            ignoring.Add("one");
            ignoring.Add("only");
            ignoring.Add("or");
            ignoring.Add("other");
            ignoring.Add("others");
            ignoring.Add("otherwise");
            ignoring.Add("ought");
            ignoring.Add("our");
            ignoring.Add("ours");
            ignoring.Add("ourselves");
            ignoring.Add("out");
            ignoring.Add("outside");
            ignoring.Add("over");
            ignoring.Add("own");
            ignoring.Add("part");
            ignoring.Add("per");
            ignoring.Add("perhaps");
            ignoring.Add("please");
            ignoring.Add("put");
            ignoring.Add("rather");
            ignoring.Add("re");
            ignoring.Add("same");
            ignoring.Add("see");
            ignoring.Add("seem");
            ignoring.Add("seemed");
            ignoring.Add("seeming");
            ignoring.Add("seems");
            ignoring.Add("several");
            ignoring.Add("she");
            ignoring.Add("should");
            ignoring.Add("show");
            ignoring.Add("side");
            ignoring.Add("since");
            ignoring.Add("so");
            ignoring.Add("some");
            ignoring.Add("somehow");
            ignoring.Add("someone");
            ignoring.Add("something");
            ignoring.Add("sometime");
            ignoring.Add("sometimes");
            ignoring.Add("somewhere");
            ignoring.Add("still");
            ignoring.Add("such");
            ignoring.Add("take");
            ignoring.Add("than");
            ignoring.Add("that");
            ignoring.Add("the");
            ignoring.Add("their");
            ignoring.Add("theirs");
            ignoring.Add("them");
            ignoring.Add("themselves");
            ignoring.Add("then");
            ignoring.Add("thence");
            ignoring.Add("there");
            ignoring.Add("thereafter");
            ignoring.Add("thereby");
            ignoring.Add("therefore");
            ignoring.Add("therein");
            ignoring.Add("thereupon");
            ignoring.Add("these");
            ignoring.Add("they");
            ignoring.Add("this");
            ignoring.Add("those");
            ignoring.Add("though");
            ignoring.Add("through");
            ignoring.Add("throughout");
            ignoring.Add("thru");
            ignoring.Add("thus");
            ignoring.Add("to");
            ignoring.Add("together");
            ignoring.Add("too");
            ignoring.Add("toward");
            ignoring.Add("towards");
            ignoring.Add("under");
            ignoring.Add("unless");
            ignoring.Add("until");
            ignoring.Add("up");
            ignoring.Add("upon");
            ignoring.Add("us");
            ignoring.Add("used");
            ignoring.Add("very");
            ignoring.Add("via");
            ignoring.Add("was");
            ignoring.Add("we");
            ignoring.Add("well");
            ignoring.Add("were");
            ignoring.Add("what");
            ignoring.Add("whatever");
            ignoring.Add("when");
            ignoring.Add("whence");
            ignoring.Add("whenever");
            ignoring.Add("where");
            ignoring.Add("whereafter");
            ignoring.Add("whereas");
            ignoring.Add("whereby");
            ignoring.Add("wherein");
            ignoring.Add("whereupon");
            ignoring.Add("wherever");
            ignoring.Add("whether");
            ignoring.Add("which");
            ignoring.Add("while");
            ignoring.Add("whither");
            ignoring.Add("who");
            ignoring.Add("whoever");
            ignoring.Add("whole");
            ignoring.Add("whom");
            ignoring.Add("whose");
            ignoring.Add("why");
            ignoring.Add("will");
            ignoring.Add("with");
            ignoring.Add("within");
            ignoring.Add("without");
            ignoring.Add("would");
            ignoring.Add("yes");
            ignoring.Add("yet");
            ignoring.Add("hey");
            ignoring.Add("you");
            ignoring.Add("your");
            ignoring.Add("yours");
            ignoring.Add("yourself");
            ignoring.Add("yourselves");

        }//




        public void answers(ArrayList add_answers  )
        {//start of method



            //greetings answers
            add_answers.Add("greeting :I'm doing well, thanks for asking! What cybersecurity topic can I help you with today?");
            add_answers.Add("greeting :I'm great today, thanks for asking! how can i help you today ?");
            add_answers.Add("greeting :Doing good! Good to hear from you.What would you like to learn about online safety today?");
            add_answers.Add("hi :Hi there! I'm your cybersecurity assistant. What can I help you with?");

            //purpose answers
            add_answers.Add("purpose :My purpose is to educate you on how to stay safe online and guide your cybersecurity questions.");
            add_answers.Add("purpose :I help users understand online safety and digital protection.");
            add_answers.Add("purpose :I assist with cybersecurity awareness and safety guidance.");
            add_answers.Add("purpose :I'm here to guide you through cybersecurity topics.");

            //cybersecurity answers
            add_answers.Add("cybersecurity :Cybersecurity is about protecting systems and networks from digital threats.");
            add_answers.Add("cybersecurity :It involves protecting devices and online accounts from attacks.");
            add_answers.Add("cybersecurity :It focuses on securing digital information and systems.");
            add_answers.Add("cybersecurity :It covers everything from securing your passwords to protecting entire company networks from hackers.");

            //phishing answers
            add_answers.Add("phishing :Phishing is a scam where attackers pretend to be trusted sources to steal information.");
            add_answers.Add("phishing :It uses fake messages or websites to trick users into revealing sensitive data.");
            add_answers.Add("phishing :Attackers use deception to make users believe they are legitimate.");
            add_answers.Add("phishing :Watch out for emails or messages with urgent language, suspicious links, or requests for personal information. These are classic phishing signs.");
            add_answers.Add("phishing :Always check the sender's email address carefully. Phishing emails often use addresses that look almost right but have small differences.");
            add_answers.Add("phishing :Never click links in unexpected emails. Instead, go directly to the website by typing the address in your browser yourself.");

            //firewall answers
            add_answers.Add("firewall :A firewall controls network traffic based on security rules.");
            add_answers.Add("firewall :It helps block unwanted access to your device or network.");
            add_answers.Add("firewall :It acts as a protective barrier between trusted and untrusted networks.");
            add_answers.Add("firewall :A firewall acts as a security guard for your network — it monitors incoming and outgoing traffic and blocks anything suspicious.");
            add_answers.Add("firewall :Think of a firewall as a barrier between your device and the internet that filters out harmful connections.");
            add_answers.Add("firewall :Keeping your firewall turned on is one of the simplest ways to protect your device from unauthorised access.");

            //password answers
            add_answers.Add("password :A password is used to secure access.");
            add_answers.Add("password :It should be strong, long and not easy to guess.");
            add_answers.Add("password :Avoid using personal details when creating one.");
            add_answers.Add("password :A strong password should be at least 12 characters long and include a mix of letters, numbers, and symbols.");
            add_answers.Add("password :Never reuse the same password across multiple accounts. If one account gets hacked, all others become vulnerable too.");
            add_answers.Add("password :Consider using a password manager, it creates and stores strong passwords for you so you don't have to remember them all.");
            add_answers.Add("password :Avoid using personal details like your name, birthday, or pet's name in your passwords. These are easy for attackers to guess.");

            //hacked account answers
            add_answers.Add("hacked account :Immediately secure your account and log out of all devices.");
            add_answers.Add("hacked account :Contact support if your account has been compromised.");
            add_answers.Add("hacked account :Enable extra security like two-factor authentication.");
            add_answers.Add("hacked account :Enable two-factor authentication right away — it adds an extra layer of security even if your password is compromised.");
            add_answers.Add("hacked account :Contact the platform's support team to report the breach and check for any unauthorised activity in your account.");
            add_answers.Add("hacked account :Check if your email has appeared in any data breaches by visiting haveibeenpwned.com — it's free and safe to use.");


            //fraud answers
            add_answers.Add("fraud :Contact your bank immediately if fraud is detected.");
            add_answers.Add("fraud :Report suspicious financial activity to the authorities.");
            add_answers.Add("fraud :Monitor your accounts for unusual activity.");
            add_answers.Add("fraud :If you suspect financial fraud, contact your bank immediately so they can freeze your card and investigate.");
            add_answers.Add("fraud :Report online fraud to your country's cybercrime authority — in South Africa that's the SAPS or the SABRIC fraud hotline.");
            add_answers.Add("fraud :Regularly check your bank statements for small unfamiliar transactions — fraudsters often test with tiny amounts first.");
            add_answers.Add("fraud :Never share your OTP (one-time PIN) with anyone, even someone claiming to be from your bank. Banks never ask for this.");

            //malicious chatbot answers
            add_answers.Add("malicious chatbot :Malicious bots often create urgency to trick users.");
            add_answers.Add("malicious chatbot :Fake chatbots may ask for sensitive information.");
            add_answers.Add("malicious chatbot :Be cautious if a bot pressures you for personal data.");
            add_answers.Add("malicious chatbot :If a chatbot makes you feel uncomfortable or pushes for personal information, close the chat and report it.");
            add_answers.Add("malicious chatbot :Be cautious of bots on social media that send unsolicited messages with links or offers that seem too good to be true.");

            //vpn answers
            add_answers.Add("vpn :A VPN helps protect your privacy on public Wi-Fi.");
            add_answers.Add("vpn :It encrypts your internet traffic for safety.");
            add_answers.Add("vpn :It improves security when using public networks.");
            add_answers.Add("vpn :A VPN hides your IP address and online activity, giving you greater privacy while browsing.");

            //Safe browsing answers
            add_answers.Add("browsing :Always check that a website has 'https' and a padlock icon before entering any personal or payment information.");
            add_answers.Add("browsing :Avoid clicking on pop-up ads or downloading software from untrusted websites — they often contain malware.");
            add_answers.Add("safe browsing :Keep your browser and its extensions up to date. Outdated browsers are a common way attackers get into your device.");

            //sentiment detection

            add_answers.Add("frustrated i understand you're frustrated. let's work through the issue step by step.");
            add_answers.Add("frustrated it's okay to feel frustrated when things aren't working. i'm here to help.");
            add_answers.Add("frustrated take a breath, we'll fix this together.");


            add_answers.Add("confused that's okay, confusion is normal. i'll explain it clearly for you.");
            add_answers.Add("confused let me break it down step by step so it makes sense.");
            add_answers.Add("confused no worries, i'll help you understand it better.");


            add_answers.Add("worried it's okay to feel worried. i'm here to help you stay safe online.");
            add_answers.Add("worried don't panic, most cybersecurity issues can be fixed quickly.");
            add_answers.Add("worried i understand your concern. let's make sure your information is safe.");


            add_answers.Add("happy that's great to hear! i'm glad things are going well.");
            add_answers.Add("happy awesome! positivity is always good.");
            add_answers.Add("happy i'm happy for you! let me know if you need anything.");


            add_answers.Add("sad i'm sorry you're feeling this way. i'm here for you.");
            add_answers.Add("sad that sounds tough, take things one step at a time.");
            add_answers.Add("sad i hope things improve soon. you can talk to me anytime.");


            add_answers.Add("angry i understand you're angry. let's try solve the issue together.");
            add_answers.Add("angry it's okay to feel angry, but i'll help you fix the problem.");
            add_answers.Add("angry take your time, i'm here to help you sort it out.");



            

        }//end of method








    }
}