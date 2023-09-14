using Amazon.Runtime;
using Amazon.Runtime.Internal.Transform;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System;
using System.Net;
using System.Text;
using System.Text.Json;
using AngleSharp;
using System.Net.Http;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using System.Security.Policy;
using AngleSharp.Dom;
using Url = AngleSharp.Dom.Url;
using System.Diagnostics;
using AngleSharp.Html.Parser;
using Timer = System.Windows.Forms.Timer;

namespace getsportcontrol
{
	public partial class Form1 : Form
	{
		string token = string.Empty;
		private IMongoCollection<sport> _mongoCollectionsport;
		private IMongoCollection<retrieves> _mongoCollectionretrieves;
		private IMongoCollection<evenActive> _mongoCollectioneventActive;
		private IMongoCollection<MarketOdds> _mongoCollectionMarketOdds;
		private MongoClient _mongoClient;
		private IMongoDatabase _database;
		private member member;
		public Form1()
		{
			InitializeComponent();

		}
		private void Form1_Load(object sender, EventArgs e)
		{
			var _mongoClient = new MongoClient("mongodb://localhost:27017");
			var _database = _mongoClient.GetDatabase("fristMongo");
			_mongoCollectionsport = _database.GetCollection<sport>("Sports");
			_mongoCollectionretrieves = _database.GetCollection<retrieves>("retrieves");
			_mongoCollectioneventActive = _database.GetCollection<evenActive>("evenActive");
			_mongoCollectionMarketOdds = _database.GetCollection<MarketOdds>("MarketOdds");
			timer2.Interval = 10000;
			timer2.Start();
			timer1.Interval = 2000000;
			timer1.Start();
		}
		private async void btn_login_Click(object sender, EventArgs e)
		{
			HttpClientHandler clientHandler = new HttpClientHandler();
			//自動解壓縮標頭
			clientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			using (HttpClient client = new HttpClient(clientHandler))
			{
				string _url = "https://www.isn88.com/membersite-api/api/member/authenticate";

				HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, _url);
				var acount = new Dictionary<string, string>()
				{
					{"username","eds_test_3"},
					{"password","1234qwer"}
				};
				var json = JsonSerializer.Serialize(acount);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				httpRequest.Content = content;
				var responce = await client.SendAsync(httpRequest);
				if (responce.IsSuccessStatusCode)
				{
					var data = JsonSerializer.Deserialize<authenticate>(await responce.Content.ReadAsStringAsync());
					token = data.token;
					MessageBox.Show("連線成功");
					_url = "https://www.isn88.com/membersite-api/api/member/info";
					httpRequest = new HttpRequestMessage(HttpMethod.Get, _url);
					httpRequest.Headers.Add("Authorization", $"{token}");
					httpRequest.Headers.Add("locale", "en_US");
					responce = await client.SendAsync(httpRequest);
					if (responce.IsSuccessStatusCode)
					{
						var memdata = await responce.Content.ReadAsStringAsync();
						member = JsonSerializer.Deserialize<member>(memdata);
						MessageBox.Show("歡迎:" + member.name);
					}
					else
						MessageBox.Show("連線失敗");
				}
				else
					MessageBox.Show("連線失敗");
			}


		}

		private async void btn_sports_Click(object sender, EventArgs e)
		{
			if (token == string.Empty)
			{
				MessageBox.Show("請登入");
			}
			else
			{
				HttpClientHandler clientHandler = new HttpClientHandler();
				clientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
				using (HttpClient client = new HttpClient(clientHandler))
				{
					string url = "https://www.isn88.com/membersite-api/api/data/sports";
					HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
					httpRequest.Headers.Add("Authorization", $"{token}");
					httpRequest.Headers.Add("locale", "en_US");
					var responce = await client.SendAsync(httpRequest);
					if (responce.IsSuccessStatusCode)
					{
						var data = await responce.Content.ReadAsStringAsync();
						var datas = JsonSerializer.Deserialize<IEnumerable<sport>>(data);
						//寫入Mongodb
						await _mongoCollectionsport.InsertManyAsync(datas);
						MessageBox.Show("已保存");
					}
					else
						MessageBox.Show("連線失敗");
				}
			}
		}

		private async void btn_Active_Click(object sender, EventArgs e)
		{
			if (token == string.Empty)
			{
				MessageBox.Show("請登入");
			}
			else
			{
				HttpClientHandler clientHandler = new HttpClientHandler();
				clientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
				using (HttpClient client = new HttpClient(clientHandler))
				{
					string url = "https://www.isn88.com/membersite-api/api/data/events/active";
					HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
					httpRequest.Headers.Add("Authorization", $"{token}");
					httpRequest.Headers.Add("locale", "en_US");
					var responce = await client.SendAsync(httpRequest);
					if (responce.IsSuccessStatusCode)
					{
						var aa = await responce.Content.ReadAsStringAsync();
						textBox1.Text = aa;
						var datas = JsonSerializer.Deserialize<int[]>(aa);
						evenActive test = new evenActive
						{
							dataArray = datas
						};
						//string res = JsonSerializer.Serialize(test);

						////寫入Mongodb
						_mongoCollectioneventActive.InsertOne(test);
						MessageBox.Show("已保存");
					}
					else
						MessageBox.Show("連線失敗");
				}
			}
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			HttpClientHandler httpClientHandler = new HttpClientHandler();
			httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			using (HttpClient client = new HttpClient(httpClientHandler))
			{
				string _url = "https://www.isn88.com/membersite-api/api/data/events/" +
					$"1/" +
					$"2/" +
					$"0/" +
					$"7/" +
					$"{member.oddsGroupId}";
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _url); ;
				httpRequestMessage.Headers.Add("Authorization", $"{token}");
				httpRequestMessage.Headers.Add("locale", "en_US");
				var responce = await client.SendAsync(httpRequestMessage);
				if (responce.IsSuccessStatusCode)
				{
					var datas = new List<MarketOdds>();
					var data = JsonSerializer.Deserialize<retrieves>(await responce.Content.ReadAsStringAsync());
					//Marketid 增加lines
					foreach (var item1 in data.schedule.leagues)
					{
						foreach (var item2 in item1.events)
						{
							foreach (var item3 in item2.markets)
							{
								if (item3.lines == null)
									continue;
								foreach (var item4 in item3.lines)
								{
									foreach (var item5 in item4.marketSelections)
									{
										var marketOdds = new MarketOdds()
										{
											date = DateTime.Now.ToString("yyyyy/MM/dd:HH:mm"),
											id = item5.id,
											name = item5.name,
											leagues = item1.name,
											eventname = item2.name,
											indicator = item5.indicator,
											handicap = item5.handicap,
											odds = item5.odds,
											decimalOdds = item5.odds,
										};
										datas.Add(marketOdds);
									}
								}
							}
						}
					}
					await _mongoCollectionMarketOdds.InsertManyAsync(datas);
					MessageBox.Show("已保存");
				}
				else
					MessageBox.Show("連線失敗");
			}

		}

		private async void button2_Click(object sender, EventArgs e)
		{

			HttpClientHandler httpClientHandler = new HttpClientHandler();
			httpClientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			using (HttpClient client = new HttpClient(httpClientHandler))
			{
				string _url = "https://www.isn88.com/membersite-api/api/data/events/last10sec/" +
					$"1/" +
					$"2/" +
					$"7/" +
					$"{member.oddsGroupId}";
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _url); ;
				httpRequestMessage.Headers.Add("Authorization", $"{token}");
				httpRequestMessage.Headers.Add("locale", "en_US");
				var responce = await client.SendAsync(httpRequestMessage);
				if (responce.IsSuccessStatusCode)
				{
					var datas = new List<MarketOdds>();
					var data = JsonSerializer.Deserialize<retrieves>(await responce.Content.ReadAsStringAsync());
					//Marketid 增加lines
					if (data.schedule.leagues.Count > 0)
					{
						foreach (var item1 in data.schedule.leagues)
						{
							foreach (var item2 in item1.events)
							{
								foreach (var item3 in item2.markets)
								{
									if (item3.lines == null)
										continue;
									foreach (var item4 in item3.lines)
									{
										foreach (var item5 in item4.marketSelections)
										{
											var marketOdds = new MarketOdds()
											{
												date = DateTime.Now.ToString("yyyy/MM/dd:HH:mm"),
												id = item5.id,
												name = item5.name,
												leagues = item1.name,
												eventname = item2.name,
												indicator = item5.indicator,
												handicap = item5.handicap,
												odds = item5.odds,
												decimalOdds = item5.odds,
											};
											datas.Add(marketOdds);
										}
									}
								}
							}
						}
						if (datas.Count > 0)
							await _mongoCollectionMarketOdds.InsertManyAsync(datas);
						else
							return;
						textBox1.Text = "已保存" + DateTime.Now.ToString("yyyy/MM/dd:HH:mm:ss");
					}
					else
						textBox1.Text = "無更新" + DateTime.Now.ToString("yyyy/MM/dd:HH:mm:ss");
				}
				else
					MessageBox.Show("連線失敗");
			}
		}

		private void timer2_Tick(object sender, EventArgs e)
		{
			button2_Click(sender, e);
		}
		private async Task refreshToken()
		{
			HttpClientHandler clientHandler = new HttpClientHandler();
			clientHandler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			using (HttpClient httpClient = new HttpClient(clientHandler))
			{
				string url = "https://www.isn88.com/membersite-api/api/member/refreshToken";
				HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
				httpRequest.Headers.Add("Authorization", $"{token}");
				var responce = await httpClient.SendAsync(httpRequest);
				if (responce.IsSuccessStatusCode)
				{
					var data = JsonSerializer.Deserialize<authenticate>(await responce.Content.ReadAsStringAsync());
					token = data.token;
				}
				else
					await refreshToken();
			}
		}

		private async void timer1_Tick(object sender, EventArgs e)
		{
			await refreshToken();
			textBox1.Text = "更新Token";
		}
	}
}