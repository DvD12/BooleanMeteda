using BlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogWPF.ViewModels
{

	public class PostViewModel : INotifyPropertyChanged
	{
		// Lista che si collegherà alla view tramite data binding
		// e prende i dati da Post.Posts, il model
		public ObservableCollection<Post> Posts { get; private set; } // Con ObservableCollection, la view si aggiorna automaticamente quando la lista cambia perché il ViewModel notifica la view del cambiamento
		// Comando che rappresenta l'azione di aggiungere un post
		public MyCommand AddPostCommand { get; private set; }

		/*
		// Queste, Title e Content, sono le proprietà che le views possono leggere e scrivere, alle quali si collegano tramite data binding
		public string Title
		{
			get
			{
				return _post.Title;
			}
			set // Quando viene modificato il Title...
			{
				// ...si può fare un controllo sul valore inserito
				//if (value.Length > 10)
				//	return;

				if (_post.Title != value)
				{
					_post.Title = value;
					// Quando viene modificata la proprietà Title, viene chiamato OnPropertyChanged
					// che scatena i callback che si sono iscritti (+=) all'evento PropertyChanged
					OnPropertyChanged(nameof(Title));
				}
			}
		}
		public string Content
		{
			get
			{
				return _post.Content;
			}
			set
			{
				if (_post.Content != value)
				{
					_post.Content = value;
					OnPropertyChanged(nameof(Content));
				}
			}
		}
		*/

		public PostViewModel() // Invocato dalla view quando ne definisce il DataContext
		{
			if (Post.Posts == null)
			{
				Post.Posts = new List<Post>()
				{
					new Post()
					{
						Title = "Post 1",
						Content = "Description"
					},
					new Post()
					{
						Title = "Post 2",
						Content = "Description"
					}
				};
			}

			this.Posts = new ObservableCollection<Post>(Post.Posts); // Copia di Post.Posts del model

			this.AddPostCommand = new MyCommand(() => // Tramite comando aggiungiamo un nuovo post alla lista Posts di questo ViewModel
			{
				Posts.Add(new Post()
				{
					Title = "New Post",
					Content = "New Description"
				});
			});
		}

		// Le views si iscrivono a questo evento per sapere quando una proprietà cambia
		// Cioè, faranno PropertyChanged += una qualche funzione che si occuperà di aggiornare la view
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			// Qui richiamiamo i callback delle funzioni che si sono iscritte all'evento PropertyChanged
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
