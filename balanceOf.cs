using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;


namespace Web3_Test
{
    class balanceOf
    {
        [Function("balanceOf", "uint256")]
        public class BalanceOfFunction : FunctionMessage
        {
            [Parameter("address", "owner", 1)]
            public string Owner { get; set; }
        }

        // The owner function message definition
       
        static async Task Main(string[] args)
        {
            var privateKey = "91218c9ef59fe4ae8fda0cb0c8086756191f311850e673721822dcabce1c99c9";
            var account = new Account(privateKey);

            //Now let's create an instance of Web3 using our account pointing to our nethereum testchain
            var web3 = new Web3(account, "https://rpc-mumbai.maticvigil.com");

            // The ERC721 contract we will be querying
            var erc721TokenContractAddress = "0x9Eb01347e477EF1B630F7B499AE74A1a2d35F1f7";

            // Example 1. Get balance of an account. This is the count of tokens that an account 
            // has from a specified contract (in this case "Gods Unchained").  
            Console.WriteLine("Input account: " );
            var accountWithSomeTokens = Console.ReadLine();
            // You can check the token balance of the above account on etherscan:
            // https://etherscan.io/token/0x6ebeaf8e8e946f0716e6533a6f2cefc83f60e8ab?a=0x5a4d185c590c5815a070ed62c278e665d137a0d9#inventory

            // Send query to contract, to find out how many tokens the owner has
            var balanceOfMessage = new BalanceOfFunction() { Owner = accountWithSomeTokens };
            var queryHandler = web3.Eth.GetContractQueryHandler<BalanceOfFunction>();
            var balance = await queryHandler
                .QueryAsync<BigInteger>(erc721TokenContractAddress, balanceOfMessage)
                .ConfigureAwait(false);
            Console.WriteLine($"Address: {accountWithSomeTokens}    holds: {balance}");
        }
    }
}
