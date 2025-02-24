using BlogWPF.Models;
using BlogWPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BlogWPF.ViewModels
{
	// Le views si possono collegare alle proprietà pubbliche di questa classe tramite data binding
	public class PostViewModel : INotifyPropertyChanged
	{		
		// Con ObservableCollection, la view si aggiorna automaticamente quando la lista cambia perché il ViewModel notifica la view del cambiamento
		private ObservableCollection<Post> _posts;
		public ObservableCollection<Post> Posts
		{
			get { return _posts; }
			private set
			{
				if (value == _posts) { return; }
				_posts = value;

				// Se non lo faccio, essendo il popolamento asincrono
				// (quindi avviene dopo il binding iniziale), la view non si aggiorna
				OnPropertyChanged(nameof(Posts));
			}
		}

		// Dichiaro i comandi a cui i pulsanti delle views si collegheranno tramite data binding
		// Li definisco nel costruttore
		// Comando che rappresenta l'azione di aggiungere un post
		public ICommand AddPostCommand { get; private set; }
		// Comando per salvare un post
		public ICommand SavePostCommand { get; private set; }
		// Comando per cancellare un post
		public ICommand DeletePostCommand { get; set; }

		private Jwt _token;
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
			// L'underscore indica una variabile di scarto, cioè una variabile che non ci interessa
			// Non è necessario assegnare il risultato di Initialize a una variabile, è solo per chiarezza sul fatto che il risultato non viene usato/non ci interessa
			_ = Initialize(); // Senza async, quindi non aspetta che Initialize finisca
			
			this.AddPostCommand = new MyCommand(async () => // Tramite comando aggiungiamo un nuovo post alla lista Posts di questo ViewModel
			{
				// Creo un post in memoria
				Post newPost = new Post()
				{
					Title = "New Post",
					Content = "New Description"
				};
				// Chiamo l'API per inserire il post nel DB del server
				var createApiResult = await ApiService.CreatePost(newPost, _token);
				if (createApiResult.Data == null)
				{
					MessageBox.Show($"ERRORE! {createApiResult.ErrorMessage}");
					return;
				}
				// Assegno il risultato dell'API (che rappresenta l'ID del nuovo post) al post in memoria
				newPost.Id = createApiResult.Data; // L'ID mi servirà per fare update e delete
				// Aggiorno la view solo in caso di successo (altrimenti sarei finito nel return di prima)
				Posts.Add(newPost);
			});

			this.SavePostCommand = new GenericCommand<Post>(async (post) =>
			{
				// Chiamo l'API per salvare il post
				var updateApiResult = await ApiService.UpdatePost(post, _token);
				if (updateApiResult.Data == 0)
				{
					MessageBox.Show($"ERRORE! {updateApiResult.ErrorMessage}");
					return;
				}
				// Se l'API ha successo, non faccio nulla perché il post è già aggiornato
			});

			this.DeletePostCommand = new GenericCommand<Post>(async post =>
			{
				var deleteApiResult = await ApiService.DeletePost(post.Id, _token);
				if (deleteApiResult.Data == 0)
				{
					MessageBox.Show($"ERRORE! {deleteApiResult.ErrorMessage}");
					return;
				}
				Posts.Remove(post); // Aggiorno il view model solo in caso di successo
			});
		}

		public async Task Initialize()
		{
			// Richiedo il JWT
			var tokenApiResult = await ApiService.GetJwtToken();
			if (tokenApiResult.Data == null)
			{
				MessageBox.Show($"ERRORE di login! {tokenApiResult.ErrorMessage}");
				return;
			}
			this._token = tokenApiResult.Data;

			// Richiedo i post
			var postsApiResult = await ApiService.GetPosts();
			if (postsApiResult.Data == null)
			{
				MessageBox.Show($"ERRORE! {postsApiResult.ErrorMessage}");
				return;
			}

			// Popolo il view model coi post da API -> aggiorna la view
			Posts = new ObservableCollection<Post>(postsApiResult.Data);
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
