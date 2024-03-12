{ pkgs }: {
	deps = [
   pkgs.openssh_hpn
		pkgs.jq.bin
    pkgs.dotnet-sdk
    pkgs.omnisharp-roslyn
	];
}