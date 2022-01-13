using System.Threading.Tasks;

void print(string a){
    Console.WriteLine(a);
};

async Task GET(string url){
    var client = new System.Net.Http.HttpClient();
    var response = await client.GetAsync(url);
    var content = await response.Content.ReadAsStringAsync();
    print(content);
};

async Task POST(string url, string data){
    var client = new System.Net.Http.HttpClient();
    var response = await client.PostAsync(url, new System.Net.Http.StringContent(data));
    var content = await response.Content.ReadAsStringAsync();
    print(content);
};

async Task PUT(string url, string data){
    var client = new System.Net.Http.HttpClient();
    var response = await client.PutAsync(url, new System.Net.Http.StringContent(data));
    var content = await response.Content.ReadAsStringAsync();
    print(content);
};

async Task DELETE(string url){
    var client = new System.Net.Http.HttpClient();
    var response = await client.DeleteAsync(url);
    var content = await response.Content.ReadAsStringAsync();
    print(content);
};

// parse args

object ParseArgs(string[] args){
    if(args.Length == 0){
        return null;
    }
    if(args.Length == 1){
        return args[0];
    }
    if(args.Length == 2){
        return new {
            url = args[0],
            data = args[1]
        };
    }
    if(args.Length == 3){
        return new {
            url = args[0],
            data = args[1],
            method = args[2]
        };
    }
    return null;
};

object a = ParseArgs(System.Environment.GetCommandLineArgs());


if(a == null){
    print("Usage: GET <url>");
    print("Usage: POST <url> <data>");
    print("Usage: PUT <url> <data>");
    print("Usage: DELETE <url>");
}else if(a is string){
    await GET(a as string);
}else if(a is System.Collections.Generic.Dictionary<string, object>){
    var aa = a as System.Collections.Generic.Dictionary<string, object>;
    if(aa["method"] as string == "GET"){
        await GET(aa["url"] as string);
    }else if(aa["method"] as string == "POST"){
        await POST(aa["url"] as string, aa["data"] as string);
    }else if(aa["method"] as string == "PUT"){
        await PUT(aa["url"] as string, aa["data"] as string);
    }else if(aa["method"] as string == "DELETE"){
        await DELETE(aa["url"] as string);
    }else{
        print("Usage: GET <url>");
        print("Usage: POST <url> <data>");
        print("Usage: PUT <url> <data>");
        print("Usage: DELETE <url>");
    }
}
