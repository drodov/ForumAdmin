   M S B A M L   `   `   `  ����   ForumController     ForumController.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  
ForumAdmin��$	��600��$	��800��.��� .+��� -   $��tabControl1��$
��Auto��$��Stretch=�$��Stretch=�$
��Auto��.@��� -   $��NewsTabItem��$
��News��.��� .+�� -   $��selectNewsGrid��$	��230��$
��Left=�$��6,6,0,0q�$	��Top=�$	��297��.+��� -   $��label1��$��NEWS:��$��28��$
��Left=�$��	40,35,0,0q�$	��Top=�$��16(�	� -   $��UsersRadioButton��$��Users��$��16��$
��Left=�$��	40,74,0,0q�$	��Top=�	� -   $��messagesRadioButton��$��Private Messages��$��16��$
��Left=�$��
40,100,0,0q�$	��Top=��� -   $��countUsersNews��$��0��$��28��$
��Left=�$��
160,69,0,0q�$	��Top=��� -	   $��countMessagesNews��$��0��$��28��$
��Left=�$��
160,95,0,0q�$	��Top=�� -
   $��showNewsGrid��$	��513��$
��Left=�$��	16,16,0,0q�$	��Top=�$	��752��$y�Hidden=�.+��� -   $��BackToNewsButton��$
��Back��$��23��$
��Left=�$��	30,30,0,0q�$	��Top=�$��75��&   System.Windows.Controls.DataGrid  -   $��NewUsersDataGrid��   AutoGenerateColumns .  $
��Auto��$
��Left=�$��	30,60,0,0q�$	��Top=�$
��Auto��   CanUserAddRows .     CanUserDeleteRows .     CanUserSortColumns .    CanUserResizeColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Login��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *
UserName+  $ 	Real Name�� . 	 �� *UserRealName+  $ Email�� . 	 �� *Email+  $	 ICQ�� . 	 �� *ICQ+  $ 
Registered�� . 	 �� *CreatedDate+  $ Last Activity�� . 	 �� *LastActivityDate+�� -   $��ForumTabItem��$��Forum��.��� .+�� -   $��showForumsGrid��$��0,0,0,0q�$
��Auto��$��Stretch=�$��Stretch=�$
��Auto��.+��� -   $��label2��$��Forums:��$
��Auto��$
��Left=�$��	30,30,0,0q�$	��Top=�$��16(��� -   $��AuthorForumButton��$��Author��$��23��$
��Left=�$��
330,60,0,0q�$	��Top=�$��75���� -   $��EditForumButton��$
��Edit��$��23��$
��Left=�$��
230,60,0,0q�$	��Top=�$��75���� -   $��AddForumButton��$	��Add��$��23��$
��Left=�$��
130,60,0,0q�$	��Top=�$��75��  -   $��ForumsDataGrid�� .  $
��auto��$
��Left=�$��	30,90,0,0q�$	��Top=�$
��auto�� .   .  .  .  
   CanUserResizeRows
 .     MinRowHeight$ 30Q� �� TextAlignment$ Center=�   $ Forum's Name�� . 	 �� *	ForumName+  $ Topics Count�� . 	 �� *TopicsCount+  $ Author�� . 	 �� *Author+  $ Date Create�� . 	 �� *
CreateDate+� -   $��showTopicsGrid��$��0,30,0,0q�$
��Auto��$��Stretch=�$��Stretch=�$
��Auto��$y�Hidden=�.+��� -   $��label3��$��Topics:��$
��Auto��$
��Left=�$��	30,30,0,0q�$	��Top=�$��16(�  -   $��TopicsDataGrid�� .  $
��auto��$
��Left=�$��	30,90,0,0q�$	��Top=�$
��auto�� .   .  . 
 .   .  $ 30Q�   $ Topic's Name�� . 	 �� *	TopicName+  $ Posts Count�� . 	 �� *
PostsCount+  $ Author�� . 	 �� *Author+  $ Date Create�� . 	 �� *
CreateDate+�� -   $��AuthorTopicButton��$��Author��$��23��$
��Left=�$��
330,60,0,0q�$	��Top=�$��75���� -   $��EditTopicButton��$
��Edit��$��23��$
��Left=�$��
230,60,0,0q�$	��Top=�$��75���� -   $��BackToForumsButton��$
��Back��$��23��$
��Left=�$��	30,60,0,0q�$	��Top=�$��75���� -   $��AddTopicButton��$	��Add��$��23��$
��Left=�$��
130,60,0,0q�$	��Top=�$��75���� -   $��CurForumlabel��$�� ��$
��Auto��$
��Left=�$��30,0,0,0q�$	��Top=�$��18(�� -   $��showPostsGrid��$��0,30,0,0q�$
��Auto��$��Stretch=�$��Stretch=�$
��Auto��$y�Hidden=� � DataContext �� .+��� -   $��CurTopiclabel��$�� ��$
��Auto��$
��Left=�$��30,0,0,0q�$	��Top=�$��18(��� -   $��label5��$��Posts:��$
��Auto��$
��Left=�$��	30,30,0,0q�$	��Top=�$��16(�  -   $��PostsDataGrid�� .  $
��auto��$
��Left=�$��	30,90,0,0q�$	��Top=�$
��auto�� .   .  . 
 .  $ 30Q�   $
 Post�� . 	 �� *Text+  $ Author�� . 	 �� *Author+  $ Added�� . 	 �� *	DateAdded+�� -    $��BackToTopicsButton��$
��Back��$��23��$
��Left=�$��	30,60,0,0q�$	��Top=�$��75���� -!   $��AddPostButton��$	��Add��$��23��$
��Left=�$��
130,60,0,0q�$	��Top=�$��75���� -"   $��EditPostButton��$
��Edit��$��23��$
��Left=�$��
230,60,0,0q�$	��Top=�$��75���� -#   $��AuthorPostButton��$��Author��$��23��$
��Left=�$��
330,60,0,0q�$	��Top=�$��75���� -$   $��UsersTabItem��$��Users��.��� .+�� -%   $��selectUserGrid��$
��Left=�$��6,0,0,0q�$	��Top=�.+��� -&   $��label4��$��Login:��$��28��$
��Left=�$��	30,30,0,0q�$	��Top=��� -'   $��loginTextBox��$��23��$
��Left=�$��	80,32,0,0q�$	��Top=�$	��280���� -(   $��FindUsersButton��$
��Find��$��23��$
��Left=�$��
375,32,0,0q�$	��Top=�$	��100��  -)   $��UsersDataGrid�� .  $
��Auto��$
��Left=�$��	30,90,0,0q�$	��Top=�$
��Auto�� .   .  $ 30Q� . 
 .     $ Login�� . 	 �� *
UserName+  $ 	Real Name�� . 	 �� *UserRealName+  $ Email�� . 	 �� *Email+  $	 ICQ�� . 	 �� *ICQ+  $ 
Registered�� . 	 �� *CreatedDate+  $ Last Activity�� . 	 �� *LastActivityDate+��  �� Orientation$ 
Horizontal=�.+�	� -*   $��AllUserRadioButton��$	��All��$��16��$
��Left=�$��	30,65,0,0q�$	��Top=� d� 	IsChecked$
 TrueY�	� -+   $��AdminRadioButton��$��Administrators��$��16��$
��Left=�$��	30,65,0,0q�$	��Top=�	� -,   $��ModerRadioButton��$��
Moderators��$��16��$
��Left=�$��	30,65,0,0q�$	��Top=�	� --   $��BanUserRadioButton��$��Banned Users��$��16��$
��Left=�$��	30,65,0,0q�$	��Top=��� -.   $��AccountTabItem��$��Account��.��� .+��� -/   $��tabControl2��$
��Auto��$��Stretch=�$��Stretch=�$
��Auto�� �� TabStripPlacement$
 Left=�.@��� -0   $��NewMesTabItem��$��New Messages��$��50��.��� .+�  -1   $��NewMesDataGrid�� .  $
��Auto��$
��Left=�$��	30,30,0,0q�$	��Top=�$
��Auto��$ 30Q� .   .  .     $
 Post�� . 	 �� *Text+  $ Author�� . 	 �� *Author+  $ Added�� . 	 �� *	DateAdded+�� -2   $��InboxTabItem��$��Inbox��$��50��.��� .+�  -3   $��InboxDataGrid�� .  $
��Auto��$
��Left=�$��	30,30,0,0q�$	��Top=�$
��Auto�� .   .  . $ 30Q�   $
 Post�� . 	 �� *Text+  $ Author�� . 	 �� *Author+  $ Added�� . 	 �� *	DateAdded+�� -4   $��SentTabItem��$
��Sent��$��50��.��� .+�  -5   $��SentDataGrid�� .  $
��Auto��$
��Left=�$��	30,30,0,0q�$	��Top=�$
��Auto�� .  $ 30Q� .  .    $
 Post�� . 	 �� *Text+  $ Sent To�� . 	 �� *Author+  $ Added�� . 	 �� *	DateAdded+�� -6   $��ProfileTabItem��$��Profile��$��50��.��� .+��� -7   $��
LoginLabel��$��Login��$��28��$
��Left=�$��	10,10,0,0q�$	��Top=��� -8   $��LoginTextBox��$��23��$
��Left=�$��	85,15,0,0q�$	��Top=�$
��Auto���� -9   $��	NameLabel��$
��Name��$��28��$
��Left=�$��	10,40,0,0q�$	��Top=��� -:   $��NameTextBox��$��23��$
��Left=�$��	85,45,0,0q�$	��Top=�$
��Auto���� -;   $��
BirthLabel��$��Birthday��$��28��$
��Left=�$��
305,40,0,0q�$	��Top=�(  "System.Windows.Controls.DatePicker  -<   $��BirthDatePicker��$��25��$	��115��$
��Left=�$	��Top=�$��
382,45,0,0q��� -=   $��	PoneLabel��$��Phone��$��28��$
��Left=�$��305,100,0,0q�$	��Top=��� ->   $��PhoneTextBox��$��23��$
��Left=�$��382,105,0,0q�$	��Top=�$
��Auto���� -?   $��WebLabel��$��Web site��$��28��$
��Left=�$��305,130,0,0q�$	��Top=��� -@   $��
WebTextBox��$��23��$
��Left=�$��382,135,0,0q�$	��Top=�$
��Auto���� -A   $��ICQLabel��$	��ICQ��$��28��$
��Left=�$��
12,100,0,0q�$	��Top=��� -B   $��
ICQTextBox��$��23��$
��Left=�$��
85,105,0,0q�$	��Top=�$
��Auto���� -C   $��	CityLabel��$
��City��$��28��$
��Left=�$��
305,70,0,0q�$	��Top=��� -D   $��CityTextBox��$��23��$
��Left=�$��
382,75,0,0q�$	��Top=�$
��Auto���� -E   $��CountryLabel��$��Country��$��28��$
��Left=�$��	10,70,0,0q�$	��Top=��� -F   $��CountryTextBox��$��23��$
��Left=�$��	85,75,0,0q�$	��Top=�$
��Auto���� -G   $��
EmailLabel��$��Email��$��28��$
��Left=�$��
10,130,0,0q�$	��Top=��� -H   $��EmailTextBox��$��23��$
��Left=�$��
85,135,0,0q�$	��Top=�$
��Auto���� -I   $��RegLabel��$��
Registered��$��28��$
��Left=�$��
12,160,0,0q�$	��Top=��� -J   $��LsstActLabel��$��Last activity��$��28��$
��Left=�$��305,160,0,0q�$	��Top=��� -K   $��
AboutLabel��$��About��$��28��$
��Left=�$��
12,190,0,0q�$	��Top=��� -L   $��ChngPassButton��$��Change password��$��23��$
��Auto��$��305,190,0,0q�$	��Top=�$
��Left=��� -M   $��LastActLabel��$�� ��$��28��$
��Left=�$��382,160,0,0q�$	��Top=��� -N   $��	ReggLabel��$�� ��$��28��$
��Left=�$��
85,160,0,0q�$	��Top=��� .+��� -O   $��AboutTextBox��$	��200�� �� TextWrapping$
 Wrap=�$
��Left=�$��
12,220,0,0q�$	��Top=�$	��450�� �� 	MaxLength$
 2000�� �� AcceptsReturn .  �� 
AcceptsTab . " �� VerticalScrollBarVisibility$
 Auto=��� -P   $��SaveProfileButton��$
��Save��$��23��$
��Left=�$��	12,10,0,0q�$	��Top=�$	��150��