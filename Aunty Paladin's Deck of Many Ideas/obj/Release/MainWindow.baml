   M S B A M L   `   `   `  ���� \  XAunty Paladins Deck of Many Ideas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DeckOfIdeasDeckOfIdeas      DeckOfIdeas.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:DeckOfIdeas     X TXceed.Wpf.Toolkit, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � Title$6  0Aunty Paladin's Deck of Many Ideas - Idea Picker�� � Icon$ DeckOfManyIdeas.ico��$	��438��$	��696�� � 
ResizeMode$ NoResize=� � 	Resources ��%    .��� .+��� $	��400��$	��680��$
��Left=�$	��Top=�.@��� $��Draw!��.��� ���	#FFE5E5E5 � ColumnDefinitions �� $	��100 ��� $��* �.+�� $��0,0,0,0q�$��0�� � RowDefinitions �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* � �� $
��100* �.+��� $��0��$��2q�$��
Plot Event��.��%  Xceed.Wpf.Toolkit.IntegerUpDown  -     Name$ PlotNumberToGenerateBox��$��Center=�$��Center=�$��40���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value$ 0X��  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 0X��� $��1��$��2q�$��
Characters��.��  -   $# CharactersNumberToGenerateBox��$��Center=�$��Center=�$��40��$ 0X�$ 0X��� $��2��$��2q�$��Setting��.��  -   $  SettingNumberToGenerateBox��$��Center=�$��Center=�$��40��$ 0X�$ 0X��� $��3��$��2q�$��High Concept��.��  -   $$ HighConceptNumberToGenerateBox��$��Center=�$��Center=�$��40��$ 0X�$ 0X��� $��4��$��2q�$
��Item��.��  -   $ ItemNumberToGenerateBox��$��Center=�$��Center=�$��40��$ 0X�$ 0X��� $��5��$��2q�$��Any Tag��.��  -   $$ AnyCategoryNumberToGenerateBox��$��Center=�$��Center=�$��40��$ 1X�$ 0X�� $��1�� �� $��* ��� $��* ��� $��26 �.+��� -   	 ��Name$	 IdeasListView��$��5q�$��0��
 �� SelectionMode$
 Single=� �� View ��  �� AllowsColumnReorder .   �� ColumnHeaderToolTip$ Idea Properties��.P��� $
��Idea�� �� Width$	 290�� �� DisplayMemberBinding ��  �� Path$
 Text��� $��Author��$	 140�� �� $ 
AuthorName��� $
��Tags��$	 100�� �� $ TagListString�� $��5q�$��1�����	#FFE5E5E5.+��� 
�������$��1q�
������� �� CornerRadius$ 4��._��� -   $��DescriptionPanel��$
��Left=�$	��540��$��4q��� .�	Details�� $��8(��� $��8(��� $
��Wrap=����� *Text+�� $��8(��� $��8(�
Author: �� $
��Wrap=����� *
AuthorName+�� Tags: �� $
��Wrap=����� *TagListString+� $��2�� �� $	��124 ��� $��* �.+��� -	   $��0��$��Bottom=�$
��Left=�$��2q�$	��120��$��22��$��
Clear List���� -
   $��1��$��Bottom=�$��Stretch=�$��2q�$��22��$��Draw from the Deck!���� $��	Edit Deck��.��� ���	#FFE5E5E5 �� $��1* ��� $��1* �.+��� -   $��DeckListView��$��5q�$��0��$
 Single=� ��  .  $ Idea Properties��.P��� $	 330�� �� $
 Text����� -   
 � Tag$
 Text��$
��Idea�� �� CellTemplate �� .R��� $��NoWrap=�$��CharacterEllipsis=��� $	 150�� �� $ 
AuthorName����� -   $ 
AuthorName��$��Author�� �� .R��� $��NoWrap=�$��CharacterEllipsis=��� $	 150�� �� $ TagListString����� -   $ TagListString��$
��Tags�� �� .R��� $��NoWrap=�$��CharacterEllipsis=�� $��1�� �� $��3* ��� $��1* ��� $��1* �.+�� $��0�� �� $��2* ��� $��1* �.+��� $��0��$
��Idea��$��4q�.���� -   $��IdeaTextBox��$��2q� �� TextWrapping$
 Wrap=��� $��1��$��Author��$��4q�.���� -   $��AuthorTextBox��$��2q��� $��1��$��4q�$
��Tags��.���� $��2q�.+��� -   $��PlotCheckBox��$��2q�.��
Plot Event�� -   $��CharactersCheckBox��$��2q�.��
Characters�� -   $��SettingCheckBox��$��2q�.��	Setting�� -   $��HighConceptCheckBox��$��2q�.��High Concept�� -   $��ItemCheckBox��$��2q�.��Item�� $��Center=�$��2��.+��� -   $��EditIdeaButton��$��4q�.��Edit Selected�� -   $��DeleteIdeaButton��$��4q�.��Delete Selected�� -   $��AddSaveButton��$��4q�.��Add�� -   $��CancelButton��$��4q�.��Cancel