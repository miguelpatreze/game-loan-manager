import { environment } from 'src/environments/environment';

export class AuthServiceConfig {
  public issuer = environment.identityServerUrl;
  public clientId = environment.clientId;
  public redirectUri = window.location.origin;
  public postLogoutRedirectUri = window.location.origin;
  public scope = 'openid games.loan.manager.web write';
  public responseType = 'code';
  public requireHttps = false;
}
