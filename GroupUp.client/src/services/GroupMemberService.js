import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class GroupMembersService {
  async getMyMemberships() {
    const res = await api.get("account/memberships")
    logger.log(res.data)
    AppState.myMemberships = res.data
  }
}

export const groupMembersService = new GroupMembersService()